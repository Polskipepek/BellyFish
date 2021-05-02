using BellyFish.Source.Game.CheckerBoard.Fields;
using BellyFish.Source.Game.Gameplay.Moves;
using BellyFish.Source.Game.Misc;
using BellyFish.Source.Game.Pawns;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace BellyFish.Source.Game.CheckerBoard {
    class CheckerboardDisplay : Form {
        public const int FieldSize = 100;

        public CheckerboardDisplay () {
            this.Size = new Size (1500, 1000);
            fields = FieldsGenerator.Instance.GenerateFields ();
            //fields = fields.Reverse2DimField ();
            DisplayedCheckboard = CheckerboardGenerator.InitializeCheckerboard ();
        }

        public Checkerboard DisplayedCheckboard { get; set; }

        Field[,] fields = new Field[8, 8];

        public Pawn SelectedPawn {
            get => _selectedPawn;
            private set {
                if (_selectedPawn != null)
                    DeselectField (_selectedPawn);
                _selectedPawn = value;

                if (_selectedPawn != null) {
                    var moves = _selectedPawn.GetAvailableMoves (DisplayedCheckboard);
                    if (moves?.Any () == true) {
                        foreach (var move in moves) {
                            SelectField (move);
                        }
                    }
                }
            }
        }
        Pawn _selectedPawn;

        private void DeselectField (Pawn pawn) {
            foreach (var move in pawn.GetAvailableMoves (DisplayedCheckboard)) {
                string name = $"{move.NewPawnPos.Letter}{move.NewPawnPos.Digit}";
                var isFieldWhite = ((int) move.NewPawnPos.Letter + move.NewPawnPos.Digit) % 2 == 0;
                var control = GetControlByName (name);
                control.BackColor = isFieldWhite ? Color.Beige : Color.Black;
            }
        }

        private void SelectField (Move move) {
            string name = $"{move.NewPawnPos.Letter}{move.NewPawnPos.Digit}";
            var control = GetControlByName (name);
            control.BackColor = Color.LightGreen;
        }


        protected override void OnPaint (PaintEventArgs e) {
            base.OnPaint (e);
            InitFieldsDisplay ();
        }

        void InitFieldsDisplay () {
            foreach (var field in fields) {

                Label labelField = new ();
                labelField.Name = $"{(char) (field.FieldPosition.Letter + 96)}{field.FieldPosition.Digit}";

                labelField.SetBounds ((int) field.FieldPosition.Letter * FieldSize, field.FieldPosition.Digit * FieldSize, FieldSize, FieldSize);
                labelField.BackColor = field.FieldColor == PawnColor.White ? Color.Beige : Color.Black;
                Controls.Add (labelField);

                var pawn = field.GetPawn (DisplayedCheckboard);
                if (pawn != null) {
                    AddPawnToField (field, pawn);
                }
                labelField.Click += (sender, e) => Field_Click (sender, e, field);
            }
        }

        private void AddPawnToField (Field field, Pawn pawn) {
            Label label = new ();
            label.SetBounds ((int) field.FieldPosition.Letter * FieldSize + FieldSize / 4, field.FieldPosition.Digit * FieldSize + FieldSize / 4, FieldSize / 2, FieldSize / 2);
            label.Text = pawn.PawnType.ToString ();
            label.BackColor = pawn.PawnColor == PawnColor.White ? Color.Yellow : Color.Brown;
            label.Font = new Font (label.Font.FontFamily, 20);
            label.ForeColor = Color.Red;
            Controls.Add (label);
            label.BringToFront ();
            label.Click += (sender, e) => OnPawnClicked (pawn);
        }

        private void Field_Click (object sender, System.EventArgs e, Field field) {
            var pawn = field.GetPawn (DisplayedCheckboard);
            SelectedPawn = null;
            if (pawn != null) {
                OnPawnClicked (pawn);
            }
        }

        void OnPawnClicked (Pawn pawn) {
            //MessageBox.Show ($"No kliklem {(char) (pawn.Position.Letter)}{pawn.Position.Digit} ");
            DisplayPawnMoves (pawn);
        }

        private void DisplayPawnMoves (Pawn pawn) {
            SelectedPawn = pawn;

        }

        Control GetControlByName (string Name) {
            foreach (Control c in this.Controls)
                if (c.Name == Name)
                    return c;

            return null;
        }

        private void InitializeComponent () {
            this.SuspendLayout ();
            // 
            // CheckerboardDisplay
            // 
            this.ClientSize = new System.Drawing.Size (1500, 1000);
            this.Name = "CheckerboardDisplay";
            this.ResumeLayout (false);

        }
    }
}
