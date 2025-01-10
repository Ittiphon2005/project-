namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    double dCash = double.Parse(tbCash.Text); // Read cash input

                    double dBeverageTotal = 0;
                    double dFoodTotal = 0;

                    // Check beverage items
                    if (CheckBoxCoffee.Checked)
                    {
                        dBeverageTotal += GetItemTotal(tbCoffeePrice.Text, tbCoffeeQuantity.Text);
                    }
                    if (CheckBoxGreenTea.Checked)
                    {
                        dBeverageTotal += GetItemTotal(tbGreenTeaPrice.Text, tbGreenTeaQuantity.Text); // Fixed textBox for quantity
                    }

                    // Check food items
                    if (CheckBoxNoodle.Checked)
                    {
                        dFoodTotal += GetItemTotal(tbNoodlePrice.Text, tbNoodleQuantity.Text);
                    }
                    if (CheckBoxPizza.Checked)
                    {
                        dFoodTotal += GetItemTotal(tbPizzaPrice.Text, tbPizzaQuantity.Text);
                    }

                    // Calculate grand total
                    double dGrandTotal = dBeverageTotal + dFoodTotal;

                    // Calculate total discount
                    double dTotalDiscount = CalculateTotalDiscount(dBeverageTotal, dFoodTotal, dGrandTotal);

                    dGrandTotal -= dTotalDiscount;

                    // Check if cash is sufficient
                    if (dCash < dGrandTotal)
                    {
                        MessageBox.Show("เงินสดไม่เพียงพอ", "ข้อผิดพลาด", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    double dChange = dCash - dGrandTotal;

                    // Display results
                    tbTotal.Text = dGrandTotal.ToString("F2");  // Display grand total
                    tbChange.Text = dChange.ToString("F2");  // Display change

                    // Calculate and display change denominations
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
            // Discount checkboxes
            double dDiscountBev = CheckBoxDiscountBeverage.Checked ? double.Parse(tbBeverageDiscount.Text) : 0;
            double dDiscountFood = CheckBoxDiscountFood.Checked ? double.Parse(tbDiscountFood.Text) : 0;
            double dDiscountAll = CheckBoxDiscountAll.Checked ? double.Parse(tbDiscountAll.Text) : 0;

            // Calculate total discount
            double dTotalDiscount = (dBeverageTotal * dDiscountBev / 100) +
                                    (dFoodTotal * dDiscountFood / 100) +
                                    (dGrandTotal * dDiscountAll / 100);

            return dTotalDiscount;
        }

        private void CalculateChangeDenominations(double change)
        {
            // Denominations for change
            double[] denominations = { 1000, 500, 100, 50, 20, 10, 5, 1, 0.50, 0.25 };
            int[] changeCount = new int[denominations.Length];
            double remainChange = change;

            // Calculate change for each denomination
            for (int i = 0; i < denominations.Length; i++)
            {
                changeCount[i] = (int)(remainChange / denominations[i]);
                remainChange %= denominations[i];
            }

            // Display the result for each denomination
            tb1000.Text = changeCount[0].ToString();
            tb500.Text = changeCount[1].ToString();
            tb100.Text = changeCount[2].ToString();
            tb50.Text = changeCount[3].ToString();
            tb20.Text = changeCount[4].ToString();
            tb10.Text = changeCount[5].ToString();
            tb5.Text = changeCount[6].ToString();
            tb1.Text = changeCount[7].ToString();

        }
    }
}
        

       
