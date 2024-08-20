using Microsoft.Maui.Platform;

namespace suma4838081
{
    public partial class MainPage : ContentPage
    {
        private readonly LocalDbService _dbService;
        public int _editResultadoId;

        public MainPage(LocalDbService dbService)
        {
            InitializeComponent();
            _dbService = dbService;
            Task.Run(async () => listview.ItemsSource = await _dbService.GetResultado());
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
           

            if(_editResultadoId == 0)
            {
                int N1 = Convert.ToInt32(Entryprimernumero.Text);
                int N2 = Convert.ToInt32(Entrysegundonumero.Text);
                int r = N1+ N2;

                labelresultado.Text = r.ToString(); 
                //agrega cliente
                await _dbService.Create(new Resultado
                {
                  
                    Numero1 = Entryprimernumero.Text,
                    Numero2 = Entrysegundonumero.Text,
                   
                    Suma = labelresultado.Text
                });

            }
            else
            {
                int N1 = Convert.ToInt32(Entryprimernumero.Text);
                int N2 = Convert.ToInt32(Entrysegundonumero.Text);
                int r = N1 + N2;

                labelresultado.Text = r.ToString();
                //editar cliente
                await _dbService.Update(new Resultado
                {
                    Id = _editResultadoId,
                    Numero1 = Entryprimernumero.Text,
                    Numero2 = Entrysegundonumero.Text,
                    Suma = labelresultado.Text
                });
                _editResultadoId =0;
            }
            Entryprimernumero.Text = string.Empty;
            Entrysegundonumero.Text = string.Empty;
            labelresultado.Text = string.Empty;

            listview.ItemsSource = await _dbService.GetResultado();
        }

        private async void listview_ItemTapped_1(object sender, ItemTappedEventArgs e)
        {
            var resultado = (Resultado)e.Item;
            var action = await DisplayActionSheet("Action","Cancel", null ,"Edit", "Delete");

            switch(action)
            {
                case "Edit":
                    _editResultadoId = resultado.Id;
                    Entryprimernumero.Text = resultado.Numero1;
                    Entrysegundonumero.Text = resultado.Numero2;
                    labelresultado.Text = resultado.Suma;
                    break;

                case "Delete":
                    await _dbService.Delete(resultado);
                    listview.ItemsSource = await _dbService.GetResultado();
                    break;
            }
        }
    }

}
