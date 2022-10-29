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
            }catch(Exception ex)
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
            catch(Exception ex)
            {
                ShowError("Eroare actualizare produse: " + ex.Message);
            }
        }

        private void AddClientToTable(string nume, string prenume, DateTime dataNasterii, string nrcard)
        {
            try
            {
                CreateSqlCommand("insert into clienti(Nume, Prenume, DataNasterii, NumarCard) values ('" + nume + "','" + prenume + "','" + dataNasterii.Date.ToString() + "','" + nrcard + "')");
                _sqlCommand.ExecuteNonQuery();
                RefreshClientData();
            }catch(Exception ex)
            {
                ShowError("Eroare adaugare client: " + ex.Message);
            }
        }

        private void DeleteClientFromTable()
        {
            try
            {
                CreateSqlCommand("delete from clienti where IdClient = " + SelectedClient.IdClient);
                _sqlCommand.ExecuteNonQuery();
                RefreshClientData();
            }
            catch (Exception ex)
            {
                ShowError("Eroare stergere client: " + ex.Message);
            }
        }

        private void AddProdusToTable(String produs, String descriere, String garantie, String stoc, String valoareUnitara)
        {
            try
            {
                ShowError("insert into produse(Produs, Descriere, Garantie, Stoc, ValoareUnitara) values ('" + produs + "','" + descriere + "'," + garantie + "," + stoc + "," + valoareUnitara + ")");
                CreateSqlCommand("insert into produse(Produs, Descriere, Garantie, Stoc, ValoareUnitara) values ('" + produs + "','" + descriere + "'," + garantie + "," + stoc + "," + valoareUnitara +")");
                _sqlCommand.ExecuteNonQuery();
                RefreshProdusData();
            }
            catch (Exception ex)
            {
                ShowError("Eroare adaugare client: " + ex.Message);
            }
        }

        private void DeleteProdusFromTable()
        {
            try
            {
                CreateSqlCommand("delete from produse where IdProdus = " + SelectedProdus.IdProdus);
                _sqlCommand.ExecuteNonQuery();
                RefreshProdusData();
            }
            catch (Exception ex)
            {
                ShowError("Eroare stergere client: " + ex.Message);
            }
        }

        private void ShowError(string msg)
        {
            new CustomMessageBox(msg, MessageType.Error, MessageButtons.Ok) { Owner = Application.Current.MainWindow }.ShowDialog();
        }
    }
}
