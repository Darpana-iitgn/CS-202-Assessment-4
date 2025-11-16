using System;
using System.Drawing;
using System.Windows.Forms;

namespace OrderPipeline
{
    // Custom EventArgs Classes
    public class OrderEventArgs : EventArgs
    {
        public string CustomerName { get; }
        public string Product { get; }
        public int Quantity { get; }

        public OrderEventArgs(string customerName, string product, int quantity)
        {
            CustomerName = customerName;
            Product = product;
            Quantity = quantity;
        }
    }

    public class ShipEventArgs : EventArgs
    {
        public string Product { get; }
        public bool Express { get; }

        public ShipEventArgs(string p, bool ex)
        {
            Product = p;
            Express = ex;
        }
    }

    public class RejectionEventArgs : EventArgs
    {
        public string Reason { get; }

        public RejectionEventArgs(string reason)
        {
            Reason = reason;
        }
    }

    public partial class OrderPipelineForm : Form
    {
        // Event Declarations with Custom Delegates
        public event EventHandler<OrderEventArgs>? OrderCreated;
        public event EventHandler<OrderEventArgs>? OrderConfirmed;
        public event EventHandler<RejectionEventArgs>? OrderRejected;
        public event EventHandler<ShipEventArgs>? OrderShipped;

        // Form Controls
        private TextBox? txtCustomerName;
        private ComboBox? cmbProduct;
        private NumericUpDown? nudQuantity;
        private Button? btnProcessOrder;
        private Button? btnShipOrder;
        private Label? lblStatus;
        private CheckBox? chkExpress;
        private GroupBox? grpOrderDetails;
        private GroupBox? grpShipping;
        private Label? lblCustomer;
        private Label? lblProduct;
        private Label? lblQuantity;

        // State Management
        private bool orderConfirmed = false;
        private string currentProduct = "";

        public OrderPipelineForm()
        {
            InitializeComponent();
            SetupEventSubscribers();
        }

        private void InitializeComponent()
        {
            this.Text = "Order Pipeline - Event Chaining Demo";
            this.Size = new Size(1250, 950);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimumSize = new Size(1250, 950);
            this.AutoScaleMode = AutoScaleMode.None;
            this.AutoSize = false;

            // Order Details Group
            grpOrderDetails = new GroupBox
            {
                Text = "Order Details",
                Location = new Point(50, 50),
                Size = new Size(1100, 380),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Padding = new Padding(15)
            };

            lblCustomer = new Label
            {
                Text = "Customer Name:",
                Location = new Point(35, 70),
                Size = new Size(315, 50),
                Font = new Font("Segoe UI", 14),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };

            txtCustomerName = new TextBox
            {
                Location = new Point(350, 65),
                Size = new Size(620, 45),
                Font = new Font("Segoe UI", 16),
                Multiline = false
            };

            lblProduct = new Label
            {
                Text = "Product:",
                Location = new Point(35, 145),
                Size = new Size(240, 50),
                Font = new Font("Segoe UI", 14),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };

            cmbProduct = new ComboBox
            {
                Location = new Point(350, 140),
                Size = new Size(620, 50),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 16)
            };
            cmbProduct.Items.AddRange(new object[] { "Laptop", "Mouse", "Keyboard" });
            cmbProduct.SelectedIndex = 0;

            lblQuantity = new Label
            {
                Text = "Quantity:",
                Location = new Point(35, 220),
                Size = new Size(240, 50),
                Font = new Font("Segoe UI", 14),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };

            nudQuantity = new NumericUpDown
            {
                Location = new Point(350, 215),
                Size = new Size(350, 50),
                Minimum = 0,
                Maximum = 100,
                Value = 1,
                Font = new Font("Segoe UI", 16)
            };

            btnProcessOrder = new Button
            {
                Text = "Process Order",
                Location = new Point(350, 295),
                Size = new Size(350, 60),
                BackColor = Color.FromArgb(0, 122, 204),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnProcessOrder.Click += BtnProcessOrder_Click;

            // Shipping Group
            grpShipping = new GroupBox
            {
                Text = "Shipping Options",
                Location = new Point(50, 460),
                Size = new Size(1100, 230),
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Padding = new Padding(15)
            };

            chkExpress = new CheckBox
            {
                Text = "Express Delivery (Dynamic Event Subscription)",
                Location = new Point(35, 65),
                Size = new Size(1000, 55),
                Checked = false,
                Font = new Font("Segoe UI", 14),
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnShipOrder = new Button
            {
                Text = "Ship Order",
                Location = new Point(350, 135),
                Size = new Size(350, 65),
                BackColor = Color.FromArgb(46, 125, 50),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false,
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Cursor = Cursors.Hand,
                TextAlign = ContentAlignment.MiddleCenter
            };
            btnShipOrder.Click += BtnShipOrder_Click;

            // Status Label
            lblStatus = new Label
            {
                Text = "Status: Ready to process orders",
                Location = new Point(50, 720),
                Size = new Size(1100, 150),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.LightYellow,
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(25),
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                AutoSize = false
            };

            // Add controls to groups
            grpOrderDetails.Controls.AddRange(new Control[] {lblCustomer, txtCustomerName, lblProduct, cmbProduct,
            lblQuantity, nudQuantity, btnProcessOrder});

            grpShipping.Controls.AddRange(new Control[] {chkExpress, btnShipOrder});

            // Add groups to form
            this.Controls.AddRange(new Control[] {grpOrderDetails, grpShipping, lblStatus});
        }

        private void SetupEventSubscribers()
        {
            // Subscribe to OrderCreated event 
            OrderCreated += ValidateOrder;
            OrderCreated += DisplayOrderInfo;

            // Subscribe to OrderConfirmed event
            OrderConfirmed += ShowConfirmation;

            // Subscribe to OrderRejected event
            OrderRejected += ShowRejection;

            // Subscribe to OrderShipped event (base subscriber)
            OrderShipped += ShowDispatch;
        }

        // Event Publishers
        private void BtnProcessOrder_Click(object? sender, EventArgs e)
        {

            if (txtCustomerName == null)
            {
                MessageBox.Show("Customer name textbox is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (cmbProduct == null)
            {
                MessageBox.Show("Product name is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (nudQuantity == null)
            {
                MessageBox.Show("Quantity is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string customerName = txtCustomerName.Text.Trim();
            string product = cmbProduct.SelectedItem?.ToString() ?? "";
            int quantity = (int)nudQuantity.Value;

            if (string.IsNullOrEmpty(customerName))
            {
                MessageBox.Show("Please enter customer name!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (btnShipOrder == null)
            {
                MessageBox.Show("Ship order button is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Reset state
            orderConfirmed = false;
            btnShipOrder.Enabled = false;

            // Raise OrderCreated event with custom EventArgs
            OrderCreated?.Invoke(this, new OrderEventArgs(customerName, product, quantity));
        }

        private void BtnShipOrder_Click(object? sender, EventArgs e)
        {
            if (!orderConfirmed)
            {
                MessageBox.Show("No confirmed order to ship!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Dynamic subscriber management based on checkbox
            if (chkExpress == null)
            {
                MessageBox.Show("Check express is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (chkExpress.Checked)
            {
                // Add NotifyCourier subscriber if not already added
                OrderShipped -= NotifyCourier; // Remove first to avoid duplicates
                OrderShipped += NotifyCourier;
            }
            else
            {
                // Remove NotifyCourier subscriber
                OrderShipped -= NotifyCourier;
            }

            // Raise OrderShipped event
            ShipEventArgs shipArgs = new ShipEventArgs(currentProduct, chkExpress.Checked);
            OrderShipped?.Invoke(this, shipArgs);
        }

        // Event Subscribers for OrderCreated
        private void ValidateOrder(object? sender, OrderEventArgs e)
        {
            if (e.Quantity <= 0)
            {
                // Trigger OrderRejected event
                OrderRejected?.Invoke(this, new RejectionEventArgs("Quantity must be greater than 0"));
                return;
            }

            if (lblStatus == null)
            {
                MessageBox.Show("Label status is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblStatus.Text = $"Status: Order Validated for {e.CustomerName}";
            lblStatus.BackColor = Color.LightBlue;

            // Chain OrderConfirmed event
            OrderConfirmed?.Invoke(this, e);
        }

        private void DisplayOrderInfo(object? sender, OrderEventArgs e)
        {
            string message = $"Order Summary:\n\n" + $"Customer: {e.CustomerName}\n" + $"Product: {e.Product}\n" + $"Quantity: {e.Quantity}";

            MessageBox.Show(message, "Order Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Event Subscriber for OrderConfirmed
        private void ShowConfirmation(object? sender, OrderEventArgs e)
        {
            if (lblStatus == null)
            {
                MessageBox.Show("Label status is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (btnShipOrder == null)
            {
                MessageBox.Show("Ship order button is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblStatus.Text = $"Status: Order Processed Successfully for {e.CustomerName}";
            lblStatus.BackColor = Color.LightGreen;

            // Enable shipping
            orderConfirmed = true;
            currentProduct = e.Product;
            btnShipOrder.Enabled = true;
        }

        // Event Subscriber for OrderRejected
        private void ShowRejection(object? sender, RejectionEventArgs e)
        {
            if (lblStatus == null)
            {
                MessageBox.Show("Label status is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblStatus.Text = $"Status: Order Invalid – Please retry. Reason: {e.Reason}";
            lblStatus.BackColor = Color.LightCoral;

            MessageBox.Show($"Order Rejected!\n\n{e.Reason}", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Event Subscribers for OrderShipped
        private void ShowDispatch(object? sender, ShipEventArgs e)
        {
            if (lblStatus == null)
            {
                MessageBox.Show("Label status is not initialized!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string deliveryType = e.Express ? "Express" : "Standard";
            lblStatus.Text = $"Status: Product dispatched: {e.Product} ({deliveryType} Delivery)";
            lblStatus.BackColor = Color.LightGreen;
        }

        private void NotifyCourier(object? sender, ShipEventArgs e)
        {
            if (e.Express)
            {
                MessageBox.Show($"Express delivery initiated for {e.Product}!\n\n" + "Courier has been notified for priority shipping.", 
                "Express Delivery", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}