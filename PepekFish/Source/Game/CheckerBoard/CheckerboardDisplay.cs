using BellyFish.Source.Game.CheckerBoard.Fields;
using BellyFish.Source.Game.Gameplay.Moves;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using BellyFish.Source.Misc;
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
            _IsCheckerboardInverted = false;
        }

        MenuStrip ms = new();
        private void InitializeToolbar() {
            ms.Dock = DockStyle.Top;

            ToolStripMenuItem toolStripMenuItem = new("Obróć szachownice", null, null, "Obróć szachownice");
            ms.Items.Add(toolStripMenuItem);
            ms.Items[0].Click += RotateCheckerboard_Clicked;
            Controls.Add(ms);
        }

        private void RotateCheckerboard_Clicked(object sender, System.EventArgs e) {
            _IsCheckerboardInverted = !_IsCheckerboardInverted;
        }

        public Checkerboard DisplayedCheckboard { get; private set; }
        bool _IsCheckerboardInverted {
            get => _isCheckerboardInverted;
            set {
                _isCheckerboardInverted = value;
                DisplayFields();
            }
        }

        bool _isCheckerboardInverted;

        public static Label Counter;

        public int Depth { get; private set; } = 5;

        Field[,] fields = new Field[8, 8];

        List<Label> _lbl_Fields = new List<Label>();
        List<Label> _lbl_Pawns = new List<Label>();

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

        private void CreateField(Field field, int x, int y) {
            Label labelField = new();
            labelField.Name = $"Field_{(char)(field.FieldPosition.Letter + 96)}{field.FieldPosition.Digit}";

            labelField.SetBounds(x * FieldSize + FieldSize, (7 - y) * FieldSize + FieldSize, FieldSize, FieldSize);
            labelField.BackColor = field.FieldColor == PawnColor.White ? Color.Beige : Color.Black;
            labelField.Text = $"Field_{(char)(field.FieldPosition.Letter + 96)}{field.FieldPosition.Digit}";
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
            label.SetBounds(GetPawnBounds(field.FieldPosition, out int y), y, FieldSize / 2, FieldSize / 2);
            label.Text = $"{pawn.PawnType.ToString().Substring(0, 1)} {(char)(field.FieldPosition.Letter + 96)}{field.FieldPosition.Digit}";
            label.Name = $"Pawn_{(char)(field.FieldPosition.Letter + 96)}{field.FieldPosition.Digit}";
            //label.Text = pawn.PawnType.ToString();
            label.BackColor = pawn.PawnColor == PawnColor.White ? Color.Yellow : Color.Brown;
            label.Font = new Font(label.Font.FontFamily, 13);
            _lbl_Pawns.Add(label);
            label.Click += (sender, e) => OnPawnClicked(pawn);
            Controls.Add(label);

            label.BringToFront();
        }

        private int GetPawnBounds(Position position, out int y) {
            y = (_IsCheckerboardInverted ? position.Digit : 9 - position.Digit) * FieldSize + FieldSize / 4;
            return (_IsCheckerboardInverted ? 9 - position.Letter : (int)(position.Letter)) * FieldSize + FieldSize / 4;
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
            char letter = position.Letter < 50 ? (char)(position.Letter + 96) : position.Letter;
            position = new(letter, position.Digit);

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

            DisplayedCheckboard.IsTerminal(out PawnColor winner);

        }

        private Field GetField(Position position) {
            foreach (var field in fields) {
                if (field.FieldPosition.Letter + 96 == position.Letter && field.FieldPosition.Digit == position.Digit)
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
                AddNumeration(x);
                AddLetteration(x);

                for (int y = 0; y < 8; y++) {
                    if (_IsCheckerboardInverted) {
                        CreateField(fields[x, 7 - y], 7 - x, y);
                    } else {
                        CreateField(fields[x, 7 - y], x, 7 - y);
                    }
                }
            }
            //InitializeToolbar();
        }

        private void AddLetteration(int i) {
            Label lbl_Letter = new();
            lbl_Letter.Text = (char)(i + 97) + "";
            lbl_Letter.Font = new Font(lbl_Letter.Font.FontFamily, 15f);
            lbl_Letter.SetBounds((_IsCheckerboardInverted ? (8 - i) : (i + 1)) * FieldSize + FieldSize / 3, FieldSize / 2, 50, 50);
            Controls.Add(lbl_Letter);
        }

        private void AddNumeration(int i) {
            Label lbl_Digit = new();
            lbl_Digit.Text = (i + 1) + "";
            lbl_Digit.Font = new Font(lbl_Digit.Font.FontFamily, 15f);
            lbl_Digit.SetBounds(FieldSize / 2, (_IsCheckerboardInverted ? (i + 1) : (8 - i)) * FieldSize + FieldSize / 3, 50, 50);
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
