using BellyFish.Source.Game.CheckerBoard.Fields;
using BellyFish.Source.Game.Gameplay.Moves;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
using BellyFish.Source.Misc.Extensions;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BellyFish.Source.Game.CheckerBoard {
    class CheckerboardDisplay : Form {
        public const int FieldSize = 100;

        public CheckerboardDisplay() {
            Application.EnableVisualStyles();
            InitializeComponent();
            InitializeToolbar();
            fields = FieldsGenerator.Instance.GenerateFields();
            DisplayedCheckboard = CheckerboardGenerator.InitializeCheckerboard();
            IsCheckerboardInverted = false;
            DisplayLabels();
        }

        private void DisplayLabels() {
            lbl_Moves = new();
            lbl_Moves.SetBounds(975, 50, 500, 450);
            lbl_Moves.BackColor = Color.White;
            lbl_Moves.Text = FormatMovesToReadable();
            lbl_Moves.Font = new Font(lbl_Moves.Font.Name, 15f);
            Controls.Add(lbl_Moves);

            lbl_Engine = new();
            lbl_Engine.SetBounds(975, 525, 500, 450);
            lbl_Engine.BackColor = Color.White;
            lbl_Engine.Text = "Engine proposition";
            Controls.Add(lbl_Engine);
        }

        private string FormatMovesToReadable() {
            string text = "Moves: \n";
            foreach (var move in DisplayedCheckboard.AllMoves) {
                string pawnLabel = "";
                switch (move.Pawn.PawnType) {
                    case PawnType.Bishop:
                        pawnLabel = "B";
                        break;
                    case PawnType.Knight:
                        pawnLabel = "N";
                        break;
                    case PawnType.Rook:
                        pawnLabel = "R";
                        break;
                    case PawnType.Queen:
                        pawnLabel = "Q";
                        break;
                    case PawnType.King:
                        pawnLabel = "K";
                        break;
                    default:
                        break;
                }
                text += $"{move.MoveNumber}. {pawnLabel}{move.PawnOriginPos}-{move.NewPawnPos} ";
            }
            return text;
        }

        readonly MenuStrip ms = new();

        public Checkerboard DisplayedCheckboard { get; private set; }
        bool IsCheckerboardInverted {
            get => _isCheckerboardInverted;
            set {
                _isCheckerboardInverted = value;
                DisplayFields();
                DisplayLabels();
            }
        }

        bool _isCheckerboardInverted;

        public int Depth { get; private set; } = 5;

        Field[,] fields = new Field[8, 8];
        readonly List<Label> _lbl_Fields = new();
        readonly List<Label> _lbl_Pawns = new();

        public Pawn SelectedPawn {
            get => _selectedPawn;
            private set {
                if (_selectedPawn != null)
                    HidePawnMoves(_selectedPawn);
                _selectedPawn = value;

                if (_selectedPawn != null) {
                    ShowSelectedPawnMoves();
                }
            }
        }

        Pawn _selectedPawn;

        public static Label lbl_Moves { get; private set; }
        public static Label lbl_Engine { get; private set; }



        private void CreateField(Field field, int x, int y) {
            Label labelField = new();
            labelField.Name = $"Field_{field.FieldPosition.Letter}{field.FieldPosition.Digit}";
            labelField.SetBounds(x * FieldSize + FieldSize, (7 - y) * FieldSize + FieldSize, FieldSize, FieldSize);
            labelField.BackColor = field.FieldColor == PawnColor.White ? Color.Beige : Color.Black;
            //labelField.Text = $"Field_{(char)(field.FieldPosition.Letter + 97)}{field.FieldPosition.Digit + 1}";
            labelField.ForeColor = Color.Red;
            _lbl_Fields.Add(labelField);
            Controls.Add(labelField);

            var pawn = field.GetPawn(DisplayedCheckboard);
            if (pawn != null) {
                CreatePawnToField(field, pawn);
            }
            labelField.Click += (sender, e) => Field_Click(field);
        }

        private void CreatePawnToField(Field field, Pawn pawn) {
            Label label = new();
            label.SetBounds(GetPawnBounds(field.FieldPosition, out int y), y, FieldSize , FieldSize );
            //label.Text = $"{pawn.PawnType.ToString().Substring(0, 1)} {(char)(field.FieldPosition.Letter + 97)}{field.FieldPosition.Digit + 1}";
            label.Name = $"Pawn_{(char)(field.FieldPosition.Letter + 97)}{field.FieldPosition.Digit + 1}";

            Image image = Image.FromFile($"C:\\Users\\Michal\\source\\repos\\BellyFish\\PepekFish\\Source\\Graphic\\{(pawn.PawnColor==PawnColor.Black?"black":"white")}-{GetPawnName(pawn.PawnType)}.bmp");
            label.Image = image.Resize(label.Width, label.Height);

            label.Font = new Font(label.Font.FontFamily, 13);
            label.Click += (sender, e) => OnPawnClicked(pawn);
            _lbl_Pawns.Add(label);
            Controls.Add(label);
            label.BringToFront();

        }

        private static string GetPawnName(PawnType pawnType) {
            switch (pawnType) {
                case PawnType.Pawn:
                    return "pawn";
                case PawnType.Bishop:
                    return "bishop";
                case PawnType.Knight:
                    return "knight";
                case PawnType.Rook:
                    return "rook";
                case PawnType.Queen:
                    return "queen";
                case PawnType.King:
                    return "king";
                default:
                    return "";
            }
        }

        private int GetPawnBounds(Position position, out int y) {
            y = (IsCheckerboardInverted ? position.Digit + 1 : 8 - position.Digit) * FieldSize ;
            return (IsCheckerboardInverted ? 8 - position.Letter : position.Letter + 1) * FieldSize ;
        }

        private void SelectField(Move move) {
            string name = $"Field_{move.NewPawnPos.Letter}{move.NewPawnPos.Digit}";
            var control = GetControlByName(name);
            control.BackColor = Color.LightGreen;
        }

        private void ShowSelectedPawnMoves() {
            var moves = _selectedPawn.GetAvailableMoves(DisplayedCheckboard);
            if (moves?.Any() == true)
                foreach (var move in moves)
                    SelectField(move);
        }

        private void HidePawnMoves(Pawn pawn) {
            foreach (var move in pawn.GetAvailableMoves(DisplayedCheckboard)) {
                string name = $"Field_{move.NewPawnPos.Letter}{move.NewPawnPos.Digit}";
                var isFieldWhite = ((int)move.NewPawnPos.Letter + move.NewPawnPos.Digit) % 2 == 0;
                var control = GetControlByName(name);
                control.BackColor = isFieldWhite ? Color.Beige : Color.Black;
            }
        }

        private bool TryMakeMove(Position position) {
            var moves = SelectedPawn.GetAvailableMoves(DisplayedCheckboard).ToList();
            var givenMove = moves.FirstOrDefault(move => move.Pawn == SelectedPawn && move.NewPawnPos == position);

            if (givenMove.Equals(default(Move))) {
                return false;
            }

            MakeMove(givenMove);
            return true;
        }

        internal void MakeMove(Move move) {
            HidePawnMoves(move.Pawn);

            DisplayedCheckboard.MakeMove(move);

            RedisplayPawns();
            lbl_Moves.Text = FormatMovesToReadable();

            DisplayedCheckboard.IsTerminal(out PawnColor winner);


        }

        private Field GetField(Position position) {
            foreach (var field in fields) {
                if (field.FieldPosition.Letter == position.Letter && field.FieldPosition.Digit == position.Digit)
                    return field;
            }
            return null;
        }

        void DisplayFields() {
            if (Controls.Count > 1) {
                Controls.Clear();
                Controls.Add(ms);
            }

            for (int x = 0; x < 8; x++) {
                AddNummeration(x);
                AddLetteration(x);

                for (int y = 0; y < 8; y++) {
                    if (IsCheckerboardInverted) {
                        CreateField(fields[x, 7 - y], 7 - x, y);
                    } else {
                        CreateField(fields[x, y], x, y);
                    }
                }
            }
            //InitializeToolbar();
        }

        private void AddLetteration(int i) {
            Label lbl_Letter = new();
            lbl_Letter.Text = (i + 1) + "";
            lbl_Letter.Font = new Font(lbl_Letter.Font.FontFamily, 15f);
            lbl_Letter.SetBounds((IsCheckerboardInverted ? (7 - i) : (i + 1)) * FieldSize + FieldSize / 3, FieldSize / 2, 50, 50);
            Controls.Add(lbl_Letter);
        }

        private void AddNummeration(int i) {
            Label lbl_Digit = new();
            lbl_Digit.Text = (i + 1) + "";
            lbl_Digit.Font = new Font(lbl_Digit.Font.FontFamily, 15f);
            lbl_Digit.SetBounds(FieldSize / 2, (IsCheckerboardInverted ? i + 1 : 8 - i) * FieldSize + FieldSize / 3, 50, 50);
            Controls.Add(lbl_Digit);
        }

        void RedisplayPawns() {
            foreach (var pawn in _lbl_Pawns) {
                Controls.Remove(pawn);
            }

            foreach (var pawn in DisplayedCheckboard.GetAllPawns()) {
                CreatePawnToField(GetField(pawn.Position), pawn);
            }
        }

        Control GetControlByName(string Name) {
            foreach (Control c in this.Controls)
                if (c.Name == Name)
                    return c;

            return null;
        }

        private void InitializeToolbar() {
            ms.Dock = DockStyle.Top;

            ms.Items.Clear();

            ToolStripMenuItem stripMenuItem_NewGame = new("Nowa gra", null, null, "Nowa gra");
            ms.Items.Add(stripMenuItem_NewGame);
            ms.Items[0].Click += NewGame_Clicked; ;

            ToolStripMenuItem stripMenu_RotateCheckerboard = new("Obróć szachownice", null, null, "Obróć szachownice");
            ms.Items.Add(stripMenu_RotateCheckerboard);
            ms.Items[1].Click += RotateCheckerboard_Clicked;

            Controls.Add(ms);
        }

        private void NewGame_Clicked(object sender, System.EventArgs e) {
            InitializeToolbar();
            fields = FieldsGenerator.Instance.GenerateFields();
            DisplayedCheckboard = CheckerboardGenerator.InitializeCheckerboard();
            IsCheckerboardInverted = false;
        }

        private void RotateCheckerboard_Clicked(object sender, System.EventArgs e) {
            IsCheckerboardInverted = !IsCheckerboardInverted;
        }

        private void InitializeComponent() {

            // 
            // CheckerboardDisplay
            // 
            this.ClientSize = new System.Drawing.Size(1500, 1000);
            this.Name = "CheckerboardDisplay";
            this.ResumeLayout(false);

        }

        private void Field_Click(Field field) {
            var pawn = field.GetPawn(DisplayedCheckboard);
            Board_OnClick(pawn, field.FieldPosition);
        }


        void OnPawnClicked(Pawn pawn) {
            if (pawn.PawnColor == DisplayedCheckboard.CurrentColorToMove) {
                SelectedPawn = pawn;
            } else if (SelectedPawn != null) {
                TryMakeMove(pawn.Position);
            }
            //MessageBox.Show ($"No kliklem {(char) (pawn.Position.Letter)}{pawn.Position.Digit} ");
        }
        void Board_OnClick(Pawn pawn, Position position) {
            if (pawn != null && pawn.PawnColor == DisplayedCheckboard.CurrentColorToMove) {
                SelectedPawn = pawn;
                return;
            }

            if (SelectedPawn == null)
                return;

            if (pawn == null) {
                TryMakeMove(position);
            } else {
                TryMakeMove(pawn.Position);
            }

        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
        }
    }
}
