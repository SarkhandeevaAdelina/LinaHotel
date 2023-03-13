using AnastasiaHouse.Forms;
using AnastasiaHouse.Properties;
using Timer = System.Windows.Forms.Timer;

namespace AnastasiaHouse
{
    public partial class MainForm : Form
    {
        private string status = "Любой";
        private Visitor selectedVisitor = null;

        public MainForm()
        {
            InitializeComponent();
            DateTimeLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
            Timer timer = new Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += new EventHandler(Timer_Tick);
            UpdateVisitorsGrid();
        }

        private void UpdateVisitorsGrid()
        {
            Visitor.Read();
            List<Visitor> visitors = Visitor.Visitors;
            if (status != "Любой")
            {
                visitors = visitors.Where(v=>v.Status== status).ToList();
            }
            string search = SearchTextBox.Text.ToLower();
            if (!string.IsNullOrEmpty(search))
            {
                visitors = visitors.Where(v => v.Number.ToString() == search || $"{v.SecondName} {v.FirstName} {v.Patronymic}".ToLower().Contains(search)).ToList();
            }
            VisitorsDataGridView.DataSource = null;
            VisitorsDataGridView.DataSource = visitors;
            VisitorsDataGridView.Columns[0].Visible = false;
            VisitorsDataGridView.Columns[1].HeaderText = "Номер";
            VisitorsDataGridView.Columns[0].Width = 30;
            VisitorsDataGridView.Columns[2].HeaderText = "Фамилия";
            VisitorsDataGridView.Columns[3].HeaderText = "Имя";
            VisitorsDataGridView.Columns[4].HeaderText = "Отчество";
            VisitorsDataGridView.Columns[5].HeaderText = "Статус";
            VisitorsDataGridView.Columns[6].HeaderText = "День рождения";
            VisitorsDataGridView.Columns[7].HeaderText = "Оплата";
            VisitorsDataGridView.Columns[8].HeaderText = "Дата заезда";
            VisitorsDataGridView.Columns[9].HeaderText = "Кол-во дней";
            VisitorsDataGridView.Columns[10].Visible = false;
            VisitorsDataGridView.Columns[11].Visible = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTimeLabel.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss");
        }

        private void SearchTextBox_TextChanged(object sender, EventArgs e)
        {
            UpdateVisitorsGrid();
        }

        private void StatusRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            status = (sender as RadioButton).Text;
            UpdateVisitorsGrid();
        }

        private void UpdateSelectedVisitor()
        {
            if (selectedVisitor != null)
            {
                IdLabel.Text = $"Номер № {selectedVisitor.Number}";
                try
                {
                    PhotoPictureBox.Image = Image.FromFile(@$"{new FileInfo(Application.ExecutablePath).Directory.Parent.Parent.Parent.FullName}\Resources\{selectedVisitor.PhotoPath}");
                }
                catch 
                {
                    PhotoPictureBox.Image = Resources.placeholder;
                }
                StatusComboBox.SelectedItem = selectedVisitor.Status;
                FullNameLabel.Text = $"{selectedVisitor.SecondName} {selectedVisitor.FirstName} {selectedVisitor.Patronymic}";
                ArrivalDateLabel.Text = selectedVisitor.ArrivalDate.ToString("dd.MM.yyyy");
                DepartureDateLabel.Text = selectedVisitor.ArrivalDate.AddDays(selectedVisitor.DaysAmount).ToString("dd.MM.yyyy");
                ShowVisitorCardButton.Enabled = true;
            }
            else
            {
                IdLabel.Text = "";
                PhotoPictureBox.Image = Resources.placeholder;
                StatusComboBox.SelectedItem = null;
                FullNameLabel.Text = "";
                ArrivalDateLabel.Text = "";
                DepartureDateLabel.Text = "";
                ShowVisitorCardButton.Enabled = false;
            }
        }

        private void VisitorsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (VisitorsDataGridView.SelectedRows.Count > 0)
            {
                selectedVisitor = VisitorsDataGridView.SelectedRows[0].DataBoundItem as Visitor;
            }
            else
            {
                selectedVisitor = null;
            }
            UpdateSelectedVisitor();
        }

        private void ShowVisitorCardButton_Click(object sender, EventArgs e)
        {
            if (selectedVisitor != null)
            {
                VisitorCardForm visitorCardForm = new VisitorCardForm(selectedVisitor);
                visitorCardForm.ShowDialog();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}