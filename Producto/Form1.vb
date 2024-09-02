Imports Excel = Microsoft.Office.Interop.Excel 'aqui se importa la libreria de excel 
Public Class Form1
    Dim rutaArchivo As String = "" 'se crea la variable global para guardar la ruta del archivo
    Public AppExcel As Excel.Application ' se crea la variable para manejar la aplicacion excel
    Public Libro As Excel.Workbook ' se crea la variable para manejar un libro excel
    Public Hoja As Excel.Worksheet ' se crea la variable para manejar la hoja excel

    'se crea esta funcion para cargar el combobox con los datos del excel 
    Public Sub Cargar()
        AppExcel = New Excel.Application 'se inicia la variable para manipular aplicaciones excel 
        'se maneja con un try catch, si el archivo contiene datos
        Try
            Libro = AppExcel.Workbooks.Open(rutaArchivo) 'se abre el libro/archivo excel, a través de la ruta ya predefinida 
            Hoja = Libro.ActiveSheet 'se toma la hoja activa del libro excel
            Dim filas As Integer = Hoja.UsedRange.Rows.Count 'se cuenta el rango de filas en uso ( solo las que tienen datos)
            If filas < 2 Then
                MsgBox("El archivo seleccionado esta vacío")
            End If

            For i = 1 To filas - 1
                cmbProductos.Items.Add(i) 'se recorren las filas y se agrega el npumero de las filas para usar como código
                'se comenzo en 1 para no tomar en cuenta el encabezado (valor=0) 
            Next
            AppExcel.Quit() 'se libera el manejo de la aplicación para que no quede activa la aplicación de excel 
            Libro = Nothing 'se borra el valor de la variable porque no se va a usar nuevamente
            Hoja = Nothing 'se borra el valor de la variable porque no se va a usar nuevamente
        Catch ex As Exception
            'en caso de error, se liberara el manejo de la aplicación y sus variables. Se mostrara un mensaje
            AppExcel.Quit()
            Libro = Nothing
            Hoja = Nothing
            MsgBox("El archivo seleccionado, NO es un archivo EXCEL")
            btnCargar.Enabled = True 'se habilita el botón, en caso de estar deshabilitado
        End Try
    End Sub

    Private Sub btnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        Dim Dialogo As New OpenFileDialog 'se agrega la variable para abrir carpeta al presionar "click"
        Dialogo.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop 'iniciamos la busqueda de archivo en la carpeta de escritorio
        If Dialogo.ShowDialog() = DialogResult.OK Then 'si presionamos el boton abrir hara lo siguiente
            rutaArchivo = Dialogo.FileName 'toma la ruta del archivo seleccionado
            Cargar() 'llamamos la funcion "cargar"
        End If
        If cmbProductos.Items.Count > 0 Then
            btnCargar.Enabled = False 'si carga el combobox con datos, deshabilita el botón 
        End If
    End Sub

    Private Sub cmbProductos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbProductos.SelectedIndexChanged
        AppExcel = New Excel.Application 'se inicia la variable para manipular aplicaciones excel  
        Libro = AppExcel.Workbooks.Open(rutaArchivo) 'se abre el libro/archivo excel, a través de la ruta ya predefinida 
        Hoja = Libro.ActiveSheet 'se toma la hoja activa del libro excel
        Dim index As Integer = cmbProductos.SelectedIndex 'tomamos el valor del item seleccionado del combobox
        Dim producto As String = Hoja.Range("B" & index + 2).Value  'obtenemos el valor de la columna B, en la fila dinámica
        Dim precio As String = Hoja.Range("F" & index + 2).Value    'ya que la matriz de index empieza en 0, se debe sumar  
        Dim stock As String = Hoja.Range("G" & index + 2).Value     'el 0 + la fila de encabezado de la hoja excel (indice 1)
        'lo cual al ser una matriz, la sumatoria de 0 + 1 = 2
        AppExcel.Quit() ' se cierra la aplicación excel para que no quede "abierta la conexión"
        Libro = Nothing ' se limpian las variables
        Hoja = Nothing ' se limpian las variables ( porque el valor de estas es nulllllllll)
        txtDescripcion.Text = producto 'en el textbox seleccionado muestra el valor de la variable
        txtPrecio.Text = precio 'en el textbox seleccionado muestra el valor de la variable
        txtStock.Text = stock 'en el textbox seleccionado muestra el valor de la variable
    End Sub
End Class
