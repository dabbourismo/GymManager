using GymManager.BackEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GymManager.FrontEnd
{
    public partial class frmReporting : Form
    {
        public frmReporting()
        {
            InitializeComponent();
        }
        public frmReporting(string Parameter, string Modifier)
        {
            InitializeComponent();
            if (Parameter == "All" && Modifier == "All")
            {
                DataSet.DataSet1 dset = new DataSet.DataSet1();
                DataTable dtable = dset.Tables.Add("Subs");
                dtable = DBLayer.RefreshTable("SELECT SubID,SubName FROM Subs");
                BarcodeAll _Report = new BarcodeAll();
                _Report.SetDataSource(dtable);
                crystalReportViewer1.ReportSource = _Report;
                crystalReportViewer1.Refresh();
            }
            if (Parameter.Length > 0 && Modifier == "Game")
            {
                DataSet.DataSet1 dset = new DataSet.DataSet1();
                DataTable dtable = dset.Tables.Add("Subs");
                dtable = DBLayer.RefreshTable(@"SELECT
                                    Subs.SubID,
                                    Subs.SubName
                                    FROM
                                    Subs ,
                                    Games
                                    INNER JOIN Reservations ON Reservations.GameID = Games.GameID AND Reservations.SubID = Subs.SubID
                                    WHERE
                                    Games.GameName ='"+Parameter+"' ");
                BarcodeAll _Report = new BarcodeAll();
                _Report.SetDataSource(dtable);
                crystalReportViewer1.ReportSource = _Report;
                crystalReportViewer1.Refresh();
            }
            if (Parameter.Length >0 && Modifier == "Name")
            {
                DataSet.DataSet1 dset = new DataSet.DataSet1();
                DataTable dtable = dset.Tables.Add("Subs");
                dtable = DBLayer.RefreshTable(@"SELECT
                                Subs.SubID,
                                Subs.SubName
                                FROM
                                Subs
                                WHERE
                                Subs.SubName LIKE '%" + Parameter + "%' ");
                BarcodeSingle _Report = new BarcodeSingle();
                _Report.SetDataSource(dtable);
                crystalReportViewer1.ReportSource = _Report;
                crystalReportViewer1.Refresh();
            }

        }
    }
}
