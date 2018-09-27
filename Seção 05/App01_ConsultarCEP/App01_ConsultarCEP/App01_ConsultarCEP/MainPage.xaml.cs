using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using App01_ConsultarCEP.Servico;
using App01_ConsultarCEP.Servico.Modelo;

namespace App01_ConsultarCEP
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            this.btnBuscarCEP.Clicked += BtnBuscarCEP_Clicked;
        }

        private void BtnBuscarCEP_Clicked(object sender, EventArgs e)
        {
            this.lblResultado.Text = string.Empty;

            if (string.IsNullOrEmpty(this.txtCEP.Text))
            {
                DisplayAlert("Erro", "CEP invalido! o CEP deve ser preenchido.", "OK");
            }
            else
            {
                string cep = this.txtCEP.Text.Trim();

                if (this.isValidCEP(cep))
                {
                    try
                    {
                        Endereco end = ViaCEPServico.BuscarEnderecoCEP(cep);

                        if (end == null)
                        {
                            DisplayAlert("Erro", "Endereco nao encontrado", "OK");
                        }
                        else
                        {
                            this.lblResultado.Text = string.Format("Endereco: {0}, {1} - {2}", end.logradouro, end.localidade, end.uf);
                        }
                    }
                    catch (Exception ex)
                    {
                        DisplayAlert("Erro Critico", ex.Message, "OK");
                    }
                }
            }
        }

        private bool isValidCEP(string cep)
        {
            bool retorno = true;
            int novoCEP = 0;

            if (cep.Length != 8)
            {
                DisplayAlert("Erro", "CEP invalido! o CEP deve conter 8 caracteres.", "OK");
                retorno = false;
            }
            else if (!int.TryParse(cep, out novoCEP))
            {
                DisplayAlert("Erro", "CEP invalido! o CEP deve conter apenas numeros.", "OK");
                retorno = false;
            }

            return retorno;
        }
    }
}
