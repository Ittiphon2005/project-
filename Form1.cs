namespace WinFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    double dCash = double.Parse(tbCash.Text);

                    double dBeverageTotal = 0;
                    double dFoodTotal = 0;

                    if (checkc.Checked)
                    {
                        dBeverageTotal += GetItemTotal(coffeeP.Text, coffeeQ.Text);
                    }
                    if (checkg.Checked)
                    {
                        dBeverageTotal += GetItemTotal(grP.Text, grQ.Text);
                    }

                    if (checkN.Checked)
                    {
                        dFoodTotal += GetItemTotal(nP.Text, nQ.Text);
                    }
                    if (checkP.Checked)
                    {
                        dFoodTotal += GetItemTotal(pP.Text, pQ.Text);
                    }

                    double dGrandTotal = dBeverageTotal + dFoodTotal;

                    double dTotalDiscount = CalculateTotalDiscount(dBeverageTotal, dFoodTotal, dGrandTotal);

                    dGrandTotal -= dTotalDiscount;

                    if (dCash < dGrandTotal)
                    {
                        MessageBox.Show("เงินสดไม่เพียงพอ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    double dChange = dCash - dGrandTotal;

                    tbTotal.Text = dGrandTotal.ToString("F2");
                    tbChange.Text = dChange.ToString("F2");

                    CalculateChangeDenominations(dChange);
                }
                catch (FormatException)
                {
                    MessageBox.Show("กรุณากรอกข้อมูลตัวเลขให้ถูกต้อง", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private double GetItemTotal(string priceText, string quantityText)
        {
            double price = 0, quantity = 0;
            try
            {
                price = double.Parse(priceText);
                quantity = double.Parse(quantityText);
            }
            catch (Exception)
            {
                price = 0;
                quantity = 0;
            }
            return price * quantity;
        }
        private double CalculateTotalDiscount(double dBeverageTotal, double dFoodTotal, double dGrandTotal)
        {
            double dDiscountBev = checkB.Checked ? double.Parse(disB.Text) : 0;
            double dDiscountFood = checkF.Checked ? double.Parse(dissF.Text) : 0;
            double dDiscountAll = checkA.Checked ? double.Parse(disall.Text) : 0;

            double dTotalDiscount = (dBeverageTotal * dDiscountBev / 100) + (dFoodTotal * dDiscountFood / 100) + (dGrandTotal * dDiscountAll / 100);

            return dTotalDiscount;
        }

        private void CalculateChangeDenominations(double change)
        {
            double[] denominations = { 1000, 500, 100, 50, 20, 10, 5, 1, 0.50, 0.25 };
            int[] changeCount = new int[denominations.Length];
            double remainChange = change;

            for (int i = 0; i < denominations.Length; i++)
            {
                changeCount[i] = (int)(remainChange / denominations[i]);
                remainChange %= denominations[i];
            }

            tb1000.Text = changeCount[0].ToString();
            tb500.Text = changeCount[1].ToString();
            tb100.Text = changeCount[2].ToString();
            tb50.Text = changeCount[3].ToString();
            tb20.Text = changeCount[4].ToString();
            tb10.Text = changeCount[5].ToString();
            tb5.Text = changeCount[6].ToString();
            tb1.Text = changeCount[7].ToString();
        }

        private void checkA_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
