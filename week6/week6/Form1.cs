using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using week6.MnbServiceReference;
using week6.Entities;
using System.Security.Cryptography;
using System.Xml;
using System.Windows.Forms.DataVisualization.Charting;

namespace week6
{
    public partial class Form1 : Form
    {
        BindingList<RateData> Rates = new BindingList<RateData>();
        BindingList<string> Currencies = new BindingList<string>();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = Rates;
            comboBox1.DataSource = Currencies;

            var mnbService = new MNBArfolyamServiceSoapClient();
            var request = new GetCurrenciesRequestBody();
            var response = mnbService.GetCurrencies(request);
            var result = response.GetCurrenciesResult;
            var xml = new XmlDocument();
            xml.LoadXml(result);

            foreach (XmlElement element in xml.DocumentElement.ChildNodes[0])
            {
                
                var currency = element.InnerText;

                Currencies.Add(currency);
                // Valuta
                // var childElement = (XmlElement)element.ChildNodes[0];
                //currency.Currency = childElement.GetAttribute("curr");
                //if (childElement == null)
                //   continue;

                // Érték
                //var unit = decimal.Parse(childElement.GetAttribute("unit"));
                //var value = decimal.Parse(childElement.InnerText);
                //if (unit != 0)
                //  currency.Value = value / unit;
            }

                RefreshData();
        }

        private void RefreshData()
        {
            Rates.Clear();

            var cC = comboBox1.SelectedItem.ToString();
            var sD = dateTimePicker1.Value.ToString();
            var eD = dateTimePicker2.Value.ToString();

                var mnbService = new MNBArfolyamServiceSoapClient();
                var request = new GetExchangeRatesRequestBody()
                {
                    currencyNames = cC,
                    startDate = sD,
                    endDate = eD
                };
                var response = mnbService.GetExchangeRates(request);
                var result = response.GetExchangeRatesResult;


                var xml = new XmlDocument();
                xml.LoadXml(result);
            foreach (XmlElement element in xml.DocumentElement)
            {
                var rate = new RateData();
                Rates.Add(rate);
                rate.Date = DateTime.Parse(element.GetAttribute("date"));

                // Valuta
                var childElement = (XmlElement)element.ChildNodes[0];
                if (childElement == null)
                    continue;
                rate.Currency = childElement.GetAttribute("curr");
                

                // Érték
                var unit = decimal.Parse(childElement.GetAttribute("unit"));
                var value = decimal.Parse(childElement.InnerText);
                if (unit != 0)
                    rate.Value = value / unit;

                chart1.DataSource = Rates;

                var series = chart1.Series[0];
                series.ChartType = SeriesChartType.Line;
                series.XValueMember = "Date";
                series.YValueMembers = "Value";
                series.BorderWidth = 2;

                var legend = chart1.Legends[0];
                legend.Enabled = false;

                var chartArea = chart1.ChartAreas[0];
                chartArea.AxisX.MajorGrid.Enabled = false;
                chartArea.AxisY.MajorGrid.Enabled = false;
                chartArea.AxisY.IsStartedFromZero = false;
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshData();
        }
    }
}
