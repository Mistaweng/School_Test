Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration

Partial Class GridView1
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            BindGrid()
        End If
    End Sub

    Protected Sub BindGrid()
        Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("spCRUD", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@BL_ID", 0)
                cmd.Parameters.AddWithValue("@BL_NUMBER", "")
                cmd.Parameters.AddWithValue("@CONSIGNEE", "")
                cmd.Parameters.AddWithValue("@BL_TYPE", "")
                cmd.Parameters.AddWithValue("@CHOICE", "Select")
                Using da As New SqlDataAdapter(cmd)
                    Dim dt As New DataTable()
                    da.Fill(dt)
                    GridView1.DataSource = dt
                    GridView1.DataBind()
                End Using
            End Using
        End Using
    End Sub

    Protected Sub Insert(ByVal sender As Object, ByVal e As EventArgs)
        Dim blNumber As String = txtBLNumberInsert.Text
        Dim consignee As String = txtConsigneeInsert.Text
        Dim blType As String = txtBLTypeInsert.Text

        Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("spCRUD", con)
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

        txtBLNumberInsert.Text = ""
        txtConsigneeInsert.Text = ""
        txtBLTypeInsert.Text = ""

        BindGrid()
    End Sub

    Protected Sub OnRowEditing(ByVal sender As Object, ByVal e As GridViewEditEventArgs)
        GridView1.EditIndex = e.NewEditIndex
        BindGrid()
    End Sub

    Protected Sub OnRowCancelingEdit(ByVal sender As Object, ByVal e As EventArgs)
        GridView1.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub OnRowUpdating(ByVal sender As Object, ByVal e As GridViewUpdateEventArgs)
        Dim row As GridViewRow = GridView1.Rows(e.RowIndex)
        Dim blId As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Values(0))
        Dim blNumber As String = TryCast(row.FindControl("txtBLNumber"), TextBox).Text
        Dim consignee As String = TryCast(row.FindControl("txtConsignee"), TextBox).Text
        Dim blType As String = TryCast(row.FindControl("txtBLType"), TextBox).Text

        Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("spCRUD", con)
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

        GridView1.EditIndex = -1
        BindGrid()
    End Sub

    Protected Sub OnRowDeleting(ByVal sender As Object, ByVal e As GridViewDeleteEventArgs)
        Dim blId As Integer = Convert.ToInt32(GridView1.DataKeys(e.RowIndex).Values(0))

        Dim constr As String = ConfigurationManager.ConnectionStrings("Conn").ConnectionString
        Using con As New SqlConnection(constr)
            Using cmd As New SqlCommand("spCRUD", con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@BL_ID", blId)
                cmd.Parameters.AddWithValue("@CHOICE", "Delete")
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
            End Using
        End Using

        BindGrid()
    End Sub

    Protected Sub OnPaging(ByVal sender As Object, ByVal e As GridViewPageEventArgs)
        GridView1.PageIndex = e.NewPageIndex
        BindGrid()
    End Sub

    Protected Sub OnRowDataBound(ByVal sender As Object, ByVal e As GridViewRowEventArgs)
        If e.Row.RowType = DataControlRowType.DataRow AndAlso GridView1.EditIndex <> e.Row.RowIndex Then
            TryCast(e.Row.Cells(4).Controls(2), LinkButton).Attributes("onclick") = "return confirm('Do you want to delete this row?');"
        End If
    End Sub
End Class
