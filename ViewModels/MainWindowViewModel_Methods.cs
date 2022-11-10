using MaterialDesignThemes.Wpf;
using PBDProject.Models;
using PBDProject.Views;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PBDProject.ViewModels
{
    public partial class MainWindowViewModel
    {
        private void CreateSqlCommand(string cmdText)
        {
            _sqlCommand = _sqlConnection.CreateCommand();
            _sqlCommand.CommandType = CommandType.Text;
            _sqlCommand.CommandText = cmdText;
        }
        private void RefreshClientData()
        {
            try
            {
                ClientiList.Clear();
                //refresh ClientiTable
                CreateSqlCommand("select * from [Clienti]");
                _sqlCommand.ExecuteNonQuery();
                SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ClientModel clientModel = new ClientModel((int)sqlDataReader["IdClient"], (string)sqlDataReader["Nume"],
                        (string)sqlDataReader["Prenume"], ((DateTime)sqlDataReader["DataNasterii"]).Date, (string)sqlDataReader["NumarCard"]);
                    ClientiList.Add(clientModel);
                }

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                ShowError("Eroare actualizare clienti: " + ex.Message);
            }

        }

        private void RefreshProdusData()
        {
            try
            {
                ProduseList.Clear();
                //refresh ProduseTable
                CreateSqlCommand("select * from [Produse]");
                _sqlCommand.ExecuteNonQuery();
                SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    ProdusModel produsModel = new ProdusModel((int)sqlDataReader["IdProdus"], (string)sqlDataReader["Produs"],
                        (string)sqlDataReader["Descriere"], (Byte)sqlDataReader["Garantie"], (Byte)sqlDataReader["Stoc"],
                        (Double)sqlDataReader["ValoareUnitara"]);
                    ProduseList.Add(produsModel);
                }

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                ShowError("Eroare actualizare produse: " + ex.Message);
            }
        }

        private void RefreshVanzareData()
        {
            try
            {
                VanzariList.Clear();
                //refresh VanzariTable
                CreateSqlCommand("select * from [Vanzari]");
                _sqlCommand.ExecuteNonQuery();
                SqlDataReader sqlDataReader = _sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    VanzareModel vanzareModel = new VanzareModel((int)sqlDataReader["IdVanzare"], (int)sqlDataReader["IdProdus"], (int)sqlDataReader["IdClient"],
                        (Byte)sqlDataReader["Cantitate"], ((DateTime)sqlDataReader["DataVanzarii"]).Date, ((DateTime)sqlDataReader["DataExpirarii"]).Date);
                    vanzareModel.NumeClient = ClientiList.Where(x => x.IdClient == vanzareModel.IdClient)?.FirstOrDefault()?.Nume;
                    vanzareModel.Produs = ProduseList.Where(x => x.IdProdus == vanzareModel.IdProdus)?.FirstOrDefault()?.Produs;

                    VanzariList.Add(vanzareModel);
                }

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                ShowError("Eroare actualizare produse: " + ex.Message);
            }
        }

        public void RefreshAllData()
        {
            RefreshClientData();
            RefreshProdusData();
            RefreshVanzareData();
        }
        private void AddClientToTable(string nume, string prenume, DateTime dataNasterii, string nrcard)
        {
            try
            {
                CreateSqlCommand("insert into [Clienti](Nume, Prenume, DataNasterii, NumarCard) values" +
                    "('" + nume + "','" + prenume + "','" + dataNasterii.Date.ToString() + "','" + nrcard + "')");
                _sqlCommand.ExecuteNonQuery();
                RefreshClientData();
            }
            catch (Exception ex)
            {
                ShowError("Eroare adăugare client: " + ex.Message);
            }
        }

        private void AddProdusToTable(String produs, String descriere, String garantie, String stoc, String valoareUnitara)
        {
            try
            {
                CreateSqlCommand("insert into [Produse](Produs, Descriere, Garantie, Stoc, ValoareUnitara) values" +
                    "('" + produs + "','" + descriere + "'," + garantie + "," + stoc + "," + valoareUnitara + ")");
                _sqlCommand.ExecuteNonQuery();
                RefreshProdusData();
            }
            catch (Exception ex)
            {
                ShowError("Eroare adăugare produs: " + ex.Message);
            }
        }

        private void AddVanzareToTable(ClientModel selectedBuyer)
        {
            try
            {
                foreach (var purchase in PurchaseModels)
                {
                    // public VanzareModel(Int32 idVanzare, Int32 idProdus, Int32 idClient, Byte cantitate, DateTime dataVanzarii, DateTime dataExpirarii)
                    CreateSqlCommand("insert into [Vanzari](IdProdus, IdClient, Cantitate, DataVanzarii, DataExpirarii) values" +
                        "(" + purchase.Produs.IdProdus + "," + selectedBuyer.IdClient + "," + purchase.Cantitate + ",'" + DateTime.Now.Date.ToString() +"','" +
                        DateTime.Now.AddMonths(purchase.Produs.Garantie) + "')");
                    _sqlCommand.ExecuteNonQuery();
                    RefreshProdusData();
                    RefreshVanzareData();
                }
            }
            catch (Exception ex)
            {
                ShowError("Eroare adăugare vânzare: " + ex.Message);
            }

        }

        private void DeleteClientFromTable()
        {
            try
            {
                CreateSqlCommand("delete from [Clienti] where IdClient = " + SelectedClient.IdClient);
                _sqlCommand.ExecuteNonQuery();
                RefreshClientData();
            }
            catch (Exception ex)
            {
                ShowError("Eroare ștergere client: " + ex.Message);
            }
        }

        private void DeleteProdusFromTable()
        {
            try
            {
                CreateSqlCommand("delete from [Produse] where IdProdus = " + SelectedProdus.IdProdus);
                _sqlCommand.ExecuteNonQuery();
                RefreshProdusData();
            }
            catch (Exception ex)
            {
                ShowError("Eroare ștergere produs: " + ex.Message);
            }
        }

        private void DeleteVanzareFromTable()
        {
            try
            {
                CreateSqlCommand("delete from [Vanzari] where IdVanzare = " + SelectedVanzare.IdVanzare);
                _sqlCommand.ExecuteNonQuery();
                RefreshVanzareData();
            }
            catch (Exception ex)
            {
                ShowError("Eroare ștergere vânzare: " + ex.Message);
            }
        }

        private void ShowError(string msg)
        {
            new CustomMessageBox(msg, MessageType.Error, MessageButtons.Ok) { Owner = Application.Current.MainWindow }.ShowDialog();
        }
    }
}
