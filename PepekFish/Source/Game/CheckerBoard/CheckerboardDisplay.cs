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
            InitializeComponent();
            fields = FieldsGenerator.Instance.GenerateFields();
            DisplayedCheckboard = CheckerboardGenerator.InitializeCheckerboard();
            DisplayFields();
        }

        private Label lbl_depth;
        private Label lbl_DepthVal;
        private Button btn_DepthDec;
        private Button btn_DepthInc;
        private Panel pnl_Depth;

        public Checkerboard DisplayedCheckboard { get; private set; }

        public int Depth { get; private set; } = 5;

        Field[,] fields = new Field[8, 8];

        List<Label> Lbl_Fields = new List<Label>();
        List<Label> Lbl_Pawns = new List<Label>();

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

        private void CreateField(Field field) {
            Label labelField = new();
            labelField.Name = $"Field_{(char)(field.FieldPosition.Letter + 96)}{field.FieldPosition.Digit}";

            labelField.SetBounds((int)field.FieldPosition.Letter * FieldSize, field.FieldPosition.Digit * FieldSize, FieldSize, FieldSize);
            labelField.BackColor = field.FieldColor == PawnColor.White ? Color.Beige : Color.Black;
            Lbl_Fields.Add(labelField);
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
            label.Name = $"Pawn_{(char)(field.FieldPosition.Letter + 96)}{field.FieldPosition.Digit}";
            label.Text = pawn.PawnType.ToString();
            label.BackColor = pawn.PawnColor == PawnColor.White ? Color.Yellow : Color.Brown;
            label.Font = new Font(label.Font.FontFamily, 20);
            Lbl_Pawns.Add(label);
            label.Click += (sender, e) => OnPawnClicked(pawn);
            Controls.Add(label);

            label.BringToFront();
        }

        private static int GetPawnBounds(Position position, out int y) {
            y = position.Digit * FieldSize + FieldSize / 4;
            return (int)position.Letter * FieldSize + FieldSize / 4;
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
            foreach (var field in fields) {
                CreateField(field);
            }
        }
        void RedisplayPawns() {
            foreach (var pawn in Lbl_Pawns) {
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
            this.lbl_depth = new System.Windows.Forms.Label();
            this.lbl_DepthVal = new System.Windows.Forms.Label();
            this.btn_DepthDec = new System.Windows.Forms.Button();
            this.btn_DepthInc = new System.Windows.Forms.Button();
            this.pnl_Depth = new System.Windows.Forms.Panel();
            this.pnl_Depth.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_depth
            // 
            this.lbl_depth.AutoSize = true;
            this.lbl_depth.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_depth.Location = new System.Drawing.Point(0, 0);
            this.lbl_depth.Name = "lbl_depth";
            this.lbl_depth.Size = new System.Drawing.Size(89, 28);
            this.lbl_depth.TabIndex = 0;
            this.lbl_depth.Text = "AI Depth";
            // 
            // lbl_DepthVal
            // 
            this.lbl_DepthVal.AutoSize = true;
            this.lbl_DepthVal.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lbl_DepthVal.Location = new System.Drawing.Point(153, 0);
            this.lbl_DepthVal.Name = "lbl_DepthVal";
            this.lbl_DepthVal.Size = new System.Drawing.Size(23, 28);
            this.lbl_DepthVal.TabIndex = 1;
            this.lbl_DepthVal.Text = "5";
            // 
            // btn_DepthDec
            // 
            this.btn_DepthDec.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_DepthDec.Location = new System.Drawing.Point(107, 0);
            this.btn_DepthDec.Name = "btn_DepthDec";
            this.btn_DepthDec.Size = new System.Drawing.Size(30, 30);
            this.btn_DepthDec.TabIndex = 2;
            this.btn_DepthDec.Text = "-";
            this.btn_DepthDec.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_DepthDec.UseVisualStyleBackColor = true;
            this.btn_DepthDec.Click += new System.EventHandler(this.DepthDec_Click);
            // 
            // btn_DepthInc
            // 
            this.btn_DepthInc.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_DepthInc.Location = new System.Drawing.Point(194, 0);
            this.btn_DepthInc.Name = "btn_DepthInc";
            this.btn_DepthInc.Size = new System.Drawing.Size(30, 30);
            this.btn_DepthInc.TabIndex = 3;
            this.btn_DepthInc.Text = "+";
            this.btn_DepthInc.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btn_DepthInc.UseVisualStyleBackColor = true;
            this.btn_DepthInc.Click += new System.EventHandler(this.DepthInc_Click);
            // 
            // pnl_Depth
            // 
            this.pnl_Depth.Controls.Add(this.lbl_depth);
            this.pnl_Depth.Controls.Add(this.btn_DepthInc);
            this.pnl_Depth.Controls.Add(this.btn_DepthDec);
            this.pnl_Depth.Controls.Add(this.lbl_DepthVal);
            this.pnl_Depth.Location = new System.Drawing.Point(1258, 12);
            this.pnl_Depth.Name = "pnl_Depth";
            this.pnl_Depth.Size = new System.Drawing.Size(230, 30);
            this.pnl_Depth.TabIndex = 4;
            // 
            // CheckerboardDisplay
            // 
            this.ClientSize = new System.Drawing.Size(1500, 1000);
            this.Controls.Add(this.pnl_Depth);
            this.Name = "CheckerboardDisplay";
            this.pnl_Depth.ResumeLayout(false);
            this.pnl_Depth.PerformLayout();
            this.ResumeLayout(false);

        }

        private void Field_Click(Field field) {
            var pawn = field.GetPawn(DisplayedCheckboard);
            if (pawn != null && pawn.PawnColor == DisplayedCheckboard.CurrentColorToMove) {
                OnPawnClicked(pawn);
            } else if (SelectedPawn != null) {
                TryMakeMove(field.FieldPosition);
            } else {
                if (_selectedPawn != null)
                    HidePawnMoves(_selectedPawn);
            }
        }


        void OnPawnClicked(Pawn pawn) {
            if (pawn.PawnColor == DisplayedCheckboard.CurrentColorToMove) {
                SelectedPawn = pawn;
            } else if (SelectedPawn != null) {
                TryMakeMove(pawn.Position);
            }
            //MessageBox.Show ($"No kliklem {(char) (pawn.Position.Letter)}{pawn.Position.Digit} ");
        }

        private void TryMakeMove(Position position) {
            var moves = SelectedPawn.GetAvailableMoves(DisplayedCheckboard);
            var givenMove = moves
                .FirstOrDefault(move => move.NewPawnPos.Letter == position.Letter + 96 && move.NewPawnPos.Digit == position.Digit);
            if (givenMove.Pawn != null) {
                MakeMove(givenMove);
            }
        }

        private void DepthDec_Click(object sender, System.EventArgs e) {
            if (Depth > 2)
                this.lbl_DepthVal.Text = --Depth + "";
        }

        private void DepthInc_Click(object sender, System.EventArgs e) {
            if (Depth > 1)
                this.lbl_DepthVal.Text = ++Depth + "";
        }

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);
        }
    }
}
