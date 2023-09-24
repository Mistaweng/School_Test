Imports System.Data.SqlClient
Imports System.Data
Imports System.Configuration

Public Class RecordPage
    Inherits System.Web.UI.Page

    'Protected Sub BindGrid()
    '    Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
    '    Using con As New SqlConnection(constr)
    '        Using cmd As New SqlCommand("spCRUDPro", con)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("@BL_ID", 0)
    '            cmd.Parameters.AddWithValue("@CHOICE", "Select")
    '            Dim adapter As New SqlDataAdapter(cmd)
    '            Dim table As New DataTable()
    '            adapter.Fill(table)

    '            ' Bind the data to the GridView
    '            gvmMain.DataSource = table
    '            gvmMain.DataBind()
    '        End Using
    '    End Using
    'End Sub

    Protected Sub BindGrid()
        Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("spCRUDPro", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@BL_ID", 0)
                cmd.Parameters.AddWithValue("@BL_NUMBER", DBNull.Value) ' Pass DBNull.Value for @BL_NUMBER
                cmd.Parameters.AddWithValue("@CONSIGNEE", DBNull.Value) ' You can also pass DBNull.Value for other parameters
                cmd.Parameters.AddWithValue("@BL_TYPE", DBNull.Value)
                cmd.Parameters.AddWithValue("@CHOICE", "Select")
                Dim adapter As New SqlDataAdapter(cmd)
                Dim table As New DataTable()
                adapter.Fill(table)

                ' Bind the data to the GridView
                gvmMain.DataSource = table
                gvmMain.DataBind()
            End Using
        End Using
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Load up the initial list of items

        Dim strConn As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Dim objConn As New SqlClient.SqlConnection(strConn)
        Dim dst As New DataSet()
        Dim strSQL As String = "Select * From TestTable order by BL_NUMBER;"
        Dim dpt As New SqlClient.SqlDataAdapter(strSQL, objConn)
        dpt.Fill(dst, "TestTables")
        Dim tblData As DataTable = dst.Tables("TestTables")
        gvmMain.DataSource = tblData
        gvmMain.DataBind()
        objConn.Close()
        tblData.Dispose()
        dpt.Dispose()
        objConn.Dispose()


    End Sub

    Protected Sub Insert(ByVal sender As Object, ByVal e As EventArgs)
        Dim blNumber As String = txtBLNumberInsert.Text
        Dim consignee As String = txtConsigneeInsert.Text
        Dim blType As String = txtBLTypeInsert.Text

        Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("spCRUDPro", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@BL_ID", 0)
                cmd.Parameters.AddWithValue("@BL_NUMBER", blNumber)
                cmd.Parameters.AddWithValue("@CONSIGNEE", consignee)
                cmd.Parameters.AddWithValue("@BL_TYPE", blType)
                cmd.Parameters.AddWithValue("@CHOICE", "Insert")
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        ' Clear text boxes
        txtBLNumberInsert.Text = ""
        txtConsigneeInsert.Text = ""
        txtBLTypeInsert.Text = ""

        ' Refresh the GridView
        BindGrid()
    End Sub

    'Protected Sub Insert(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim blNumber As String = txtBLNumberInsert.Text
    '    Dim consignee As String = txtConsigneeInsert.Text
    '    Dim blType As String = txtBLTypeInsert.Text

    '    Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
    '    Using con As New SqlConnection(constr)
    '        Using cmd As New SqlCommand("spCRUDPro", con)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("@BL_ID", 0)
    '            cmd.Parameters.AddWithValue("@BL_NUMBER", blNumber)
    '            cmd.Parameters.AddWithValue("@CONSIGNEE", consignee)
    '            cmd.Parameters.AddWithValue("@BL_TYPE", blType)
    '            cmd.Parameters.AddWithValue("@CHOICE", "Insert")
    '            con.Open()
    '            cmd.ExecuteNonQuery()
    '            con.Close()
    '        End Using
    '    End Using

    '    txtBLNumberInsert.Text = ""
    '    txtConsigneeInsert.Text = ""
    '    txtBLTypeInsert.Text = ""

    '    'BindGrid()
    'End Sub

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        gvmMain.EditIndex = e.NewEditIndex
        'BindGrid()
    End Sub

    Protected Sub OnRowCancelingEdit(ByVal sender As Object, ByVal e As EventArgs)
        gvmMain.EditIndex = -1
        'BindGrid()
    End Sub


    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = gvmMain.Rows(e.RowIndex)
        Dim blId As Integer = Convert.ToInt32(gvmMain.DataKeys(e.RowIndex).Values(0))
        Dim blNumber As String = TryCast(row.FindControl("txtBLNumber"), TextBox).Text
        Dim consignee As String = TryCast(row.FindControl("txtConsignee"), TextBox).Text
        Dim blType As String = TryCast(row.FindControl("txtBLType"), TextBox).Text

        Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("spCRUDPro", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@BL_ID", blId)
                cmd.Parameters.AddWithValue("@BL_NUMBER", blNumber)
                cmd.Parameters.AddWithValue("@CONSIGNEE", consignee)
                cmd.Parameters.AddWithValue("@BL_TYPE", blType)
                cmd.Parameters.AddWithValue("@CHOICE", "Update")
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        gvmMain.EditIndex = -1
        'BindGrid()
    End Sub

    'Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
    '    Dim blId As Integer = Convert.ToInt32(gvmMain.DataKeys(e.RowIndex).Values(0))

    '    Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
    '    Using con As New SqlConnection(constr)
    '        Using cmd As New SqlCommand("spCRUDPro", con)
    '            cmd.CommandType = CommandType.StoredProcedure
    '            cmd.Parameters.AddWithValue("@BL_ID", blId)
    '            cmd.Parameters.AddWithValue("@CHOICE", "Delete")
    '            con.Open()
    '            cmd.ExecuteNonQuery()
    '            con.Close()
    '        End Using
    '    End Using

    '    'BindGrid()
    'End Sub

    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim blId As Integer = Convert.ToInt32(gvmMain.DataKeys(e.RowIndex).Values(0))

        Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("spCRUDPro", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@BL_ID", blId)
                cmd.Parameters.AddWithValue("@CHOICE", "Delete")
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        ' Refresh the GridView
        BindGrid()
    End Sub

    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        gvmMain.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow AndAlso gvmMain.EditIndex <> e.Row.RowIndex Then
            TryCast(e.Row.Cells(4).Controls(2), LinkButton).Attributes("onclick") = "return confirm('Do you want to delete this row?');"
        End If
    End Sub

End Class