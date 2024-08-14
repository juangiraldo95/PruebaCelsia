using OfficeOpenXml;
using System.Globalization;
using PruebaCelsia.Data;

public class ExcelService
{
    private readonly BaseContext _context;

    public ExcelService(BaseContext context)
    {
        _context = context;
    }

    public async Task ImportDataFromExcelAsync(string filePath)
    {
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[0];

        var rowCount = worksheet.Dimension.Rows;

        for (int row = 2; row <= rowCount; row++) // Assuming the first row is headers
        {
            try
            {
                var tipoTransaccion = worksheet.Cells[row, 1].Text; // Columna A
                var nombreCliente = worksheet.Cells[row, 2].Text; // Columna B
                var numeroIdentificacion = worksheet.Cells[row, 3].Text; // Columna C
                var direccion = worksheet.Cells[row, 4].Text; // Columna D
                var telefono = worksheet.Cells[row, 5].Text; // Columna E
                var correoElectronico = worksheet.Cells[row, 6].Text; // Columna F
                var plataformaUtilizada = worksheet.Cells[row, 7].Text; // Columna G
                var numeroFactura = worksheet.Cells[row, 8].Text; // Columna H
                var periodoFacturacion = worksheet.Cells[row, 9].Text; // Columna I
                var montoFacturado = decimal.Parse(worksheet.Cells[row, 10].Text, CultureInfo.InvariantCulture); // Columna J
                var montoPagado = decimal.Parse(worksheet.Cells[row, 11].Text, CultureInfo.InvariantCulture); // Columna K

                // Buscar o crear cliente
                var cliente = _context.Clientes.FirstOrDefault(c => c.NumeroIdentificacion == numeroIdentificacion);
                if (cliente == null)
                {
                    cliente = new Cliente
                    {
                        Nombre = nombreCliente,
                        NumeroIdentificacion = numeroIdentificacion,
                        Direccion = direccion,
                        Telefono = telefono,
                        CorreoElectronico = correoElectronico
                    };
                    _context.Clientes.Add(cliente);
                    await _context.SaveChangesAsync();
                }

                // Buscar o crear plataforma
                var plataforma = _context.Plataformas.FirstOrDefault(p => p.Nombre == plataformaUtilizada);
                if (plataforma == null)
                {
                    plataforma = new Plataforma { Nombre = plataformaUtilizada };
                    _context.Plataformas.Add(plataforma);
                    await _context.SaveChangesAsync();
                }

                // Crear transacción
                var transaccion = new Transaccion
                {
                    FechaHora = DateTime.Now,
                    Monto = montoFacturado,
                    Estado = "Pendiente",
                    Tipo = tipoTransaccion,
                    ClienteId = cliente.Id,
                    PlataformaId = plataforma.Id
                };
                _context.Transacciones.Add(transaccion);
                await _context.SaveChangesAsync();

                // Crear factura
                var factura = new Factura
                {
                    NumeroFactura = numeroFactura,
                    PeriodoFacturacion = periodoFacturacion,
                    MontoFacturado = montoFacturado,
                    MontoPagado = montoPagado,
                    TransaccionId = transaccion.Id
                };
                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();
            }
            catch (FormatException ex)
            {
                // Manejar el error de formato aquí (e.g., log, mostrar mensaje al usuario)
                Console.WriteLine($"Error al procesar la fila {row}: {ex.Message}");
            }
        }
    }
}
