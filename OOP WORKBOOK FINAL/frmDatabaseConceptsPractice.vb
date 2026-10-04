Public Class frmDatabaseConceptsPractice
    Inherits Form

    Private ReadOnly nameInput As New TextBox With {.Location = New Point(24, 48), .Width = 220}
    Private ReadOnly priceInput As New TextBox With {.Location = New Point(270, 48), .Width = 130}
    Private ReadOnly productTable As New DataTable("Products")
    Private ReadOnly productGrid As New DataGridView With {.Location = New Point(24, 130), .Size = New Size(500, 220), .ReadOnly = True, .AllowUserToAddRows = False, .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill}
    Private ReadOnly outputLabel As New Label With {.Location = New Point(24, 95), .AutoSize = True, .ForeColor = Color.White}

    Public Sub New()
        Text = "Database Concepts Practice"
        ClientSize = New Size(560, 380)
        StartPosition = FormStartPosition.CenterParent
        BackColor = Color.FromArgb(18, 40, 72)
        productTable.Columns.Add("Product", GetType(String))
        productTable.Columns.Add("Price", GetType(Decimal))
        productGrid.DataSource = productTable

        Controls.Add(New Label With {.Text = "Product:", .Location = New Point(24, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(nameInput)
        Controls.Add(New Label With {.Text = "Price:", .Location = New Point(270, 23), .AutoSize = True, .ForeColor = Color.White})
        Controls.Add(priceInput)
        Dim addButton As New Button With {.Text = "Add Record", .Location = New Point(420, 46), .Width = 100, .BackColor = Color.FromArgb(27, 75, 125), .ForeColor = Color.White, .FlatStyle = FlatStyle.Flat}
        AddHandler addButton.Click, AddressOf AddRecord
        Controls.Add(addButton)
        Controls.Add(outputLabel)
        Controls.Add(productGrid)
    End Sub

    Private Sub AddRecord(sender As Object, e As EventArgs)
        Dim price As Decimal
        If String.IsNullOrWhiteSpace(nameInput.Text) OrElse Not Decimal.TryParse(priceInput.Text, price) Then
            outputLabel.Text = "Enter a product name and a valid price."
            Return
        End If

        productTable.Rows.Add(nameInput.Text.Trim(), price)
        outputLabel.Text = $"Record added. Total records: {productTable.Rows.Count}."
        nameInput.Clear()
        priceInput.Clear()
    End Sub
End Class
