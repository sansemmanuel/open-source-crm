using System;
using System.Collections.Generic;
using Dominio;
using System.Data.SqlClient;
using System.Windows;
using System.Linq;
using System.Data;
using System.Globalization;

namespace Negocio
{
    public struct RevenueByDate
    {
        public string Date { get; set; }
        public decimal TotalAmount { get; set; }
    }
    public struct MemberAttendance
    {
        public string Date { get; set; }
        public int Quantity { get; set; }
    }
    public class MemberBusiness
    {
        #region FIELDS AND PROPS
        private DateTime startDate;
        private DateTime endDate;
        private int numberDays;
        private DateTime fromDate;
        private DateTime toDate;
        public decimal Income { get; private set; }
        public int TranNumber { get; private set; }
        public int MembersNum { get; set; }
        public int TotalVisitors { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
        public List<KeyValuePair<string, int>> TopProductsList { get; private set; }
        public List<KeyValuePair<string, string>> UnderstockList = new List<KeyValuePair<string, string>>();
        public List<KeyValuePair<string, string>> AttendanceDateList = new List<KeyValuePair<string, string>>();

        public List<MemberAttendance> MemberAttendance { get; private set; }
        public List<RevenueByDate> GrossRevenueList { get; private set; }

        #endregion


        private string connectionString = "";
//Data Source=(local)\\SQLEXPRESS;Initial Catalog=sparktech;Integrated Security=True;

        #region Conection and Ex
        private SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
        private void HandleSqlException(SqlException sqlEx)
        {
            Console.WriteLine("SQL Error: " + sqlEx.Message);
        }
        private void HandleException(Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
        #endregion

        #region SELECT Methods
        public List<Member> Listar()
        {
            List<Member> lista = new List<Member>();

            using (SqlConnection conexion = GetConnection())
            {
                SqlCommand comando = new SqlCommand("SELECT c.clienteId, c.nombreCliente, c.apellidoCliente, c.dniCliente, c.nacimientoCliente, c.pesoCliente, c.alturaCliente, c.generoCliente, c.telefonoCliente, c.emailCliente FROM dbo.Clientes AS c INNER JOIN dbo.Membresias AS m ON c.clienteId = m.clienteId WHERE m.vencidoMembresia = 0;", conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Member aux = new Member
                        {
                            clienteId = (int)lector["clienteId"],
                            nombreCliente = (string)lector["nombreCliente"],
                            apellidoCliente = (string)lector["apellidoCliente"],
                            dniCliente = (string)lector["dniCliente"],
                            nacimientoCliente = (DateTime)lector["nacimientoCliente"],
                            pesoCliente = (int)lector["pesoCliente"],
                            alturaCliente = (decimal)lector["alturaCliente"],
                            generoCliente = (string)lector["generoCliente"],
                            telefonoCliente = (string)lector["telefonoCliente"],
                            emailCliente = (string)lector["emailCliente"]
                        };

                        lista.Add(aux);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            return lista;
        }

        public List<MembershipType> LoadMemberships()
        {
            List<MembershipType> list = new List<MembershipType>();

            using (SqlConnection conexion = GetConnection())
            {
                SqlCommand comando = new SqlCommand("SELECT tipoMembresiaId, membresiaTipo, membresiaCosto FROM dbo.MembresiasTipo;", conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        MembershipType aux = new MembershipType
                        {
                            tipoMembresiaId = (int)lector["tipoMembresiaId"],
                            membresiaTipo = (string)lector["membresiaTipo"],
                            membresiaCosto = (decimal)lector["membresiaCosto"]
                        };
                        list.Add(aux);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            return list;
        }

        public List<Memberships> LoadPaidMembershipData()
        {
            List<Memberships> list = new List<Memberships>();
            using (SqlConnection conexion = GetConnection())
            {
                SqlCommand comando = new SqlCommand(@"
                    SELECT c.clienteId, c.nombreCliente, c.apellidoCliente, m.membresiaId, m.inicioMembresia, m.finMembresia, m.vencidoMembresia, m.tipoMembresiaId, m.pagoMembresia,
                    (SELECT COUNT(DISTINCT c2.clienteId) 
                    FROM dbo.Clientes c2 
                    JOIN dbo.Membresias m2 ON c2.clienteId = m2.clienteId
                    WHERE m2.pagoMembresia = 1 AND m2.vencidoMembresia = 0) AS activeMembers
                    FROM dbo.Clientes c
                    JOIN dbo.Membresias m ON c.clienteId = m.clienteId
                    WHERE m.pagoMembresia = 1 AND m.vencidoMembresia = 0;", conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader lector = comando.ExecuteReader();
                    while (lector.Read())
                    {

                        Memberships aux = new Memberships
                        {
                            clienteId = (int)lector["clienteId"],
                            nombreCliente = (string)lector["nombreCliente"],
                            membresiaId = (int)lector["membresiaId"],
                            inicioMembresia = (DateTime)lector["inicioMembresia"],
                            finMembresia = (DateTime)lector["finMembresia"],
                            vencidoMembresia = (bool)lector["vencidoMembresia"],
                            tipoMembresiaId = (int)lector["tipoMembresiaId"],
                            pagoMembresia = (bool)lector["pagomembresia"],
                            apellidoCliente = (string)lector["apellidoCliente"],


                        };
                        list.Add(aux);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            return list;
        }

        public List<Memberships> LoadOverdueMembershipData()
        {
            List<Memberships> list = new List<Memberships>();

            using (SqlConnection conexion = GetConnection())
            {
                SqlCommand comando = new SqlCommand(@"
                    SELECT c.clienteId, c.nombreCliente, c.apellidoCliente, m.membresiaId, m.inicioMembresia, m.finMembresia, m.vencidoMembresia, m.tipoMembresiaId, m.pagoMembresia
                    FROM dbo.Clientes c
                    JOIN dbo.Membresias m ON c.clienteId = m.clienteId
                    WHERE m.pagoMembresia = 0 AND m.vencidoMembresia = 1;", conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Memberships aux = new Memberships
                        {
                            clienteId = (int)lector["clienteId"],
                            nombreCliente = (string)lector["nombreCliente"],
                            apellidoCliente = (string)lector["apellidoCliente"],
                            membresiaId = (int)lector["membresiaId"],
                            inicioMembresia = (DateTime)lector["inicioMembresia"],
                            finMembresia = (DateTime)lector["finMembresia"],
                            vencidoMembresia = (bool)lector["vencidoMembresia"],
                            tipoMembresiaId = (int)lector["tipoMembresiaId"],
                            pagoMembresia = (bool)lector["pagomembresia"]
                        };
                        list.Add(aux);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            return list;
        }

        public List<Memberships> LoadPendingMembershipData()
        {
            List<Memberships> list = new List<Memberships>();

            using (SqlConnection conexion = GetConnection())
            {
                SqlCommand comando = new SqlCommand(@"
                    SELECT c.clienteId, c.nombreCliente, c.apellidoCliente, m.membresiaId, m.inicioMembresia, m.finMembresia, m.vencidoMembresia, m.tipoMembresiaId, m.pagoMembresia, m.pagoPendienteMembresia, c.telefonoCliente
                    FROM dbo.Clientes c
                    JOIN dbo.Membresias m ON c.clienteId = m.clienteId
                    WHERE m.pagoMembresia = 0 AND m.pagoPendienteMembresia = 1;", conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Memberships aux = new Memberships
                        {
                            clienteId = (int)lector["clienteId"],
                            nombreCliente = (string)lector["nombreCliente"],
                            apellidoCliente = (string)lector["apellidoCliente"],
                            membresiaId = (int)lector["membresiaId"],
                            inicioMembresia = (DateTime)lector["inicioMembresia"],
                            finMembresia = (DateTime)lector["finMembresia"],
                            vencidoMembresia = (bool)lector["vencidoMembresia"],
                            tipoMembresiaId = (int)lector["tipoMembresiaId"],
                            pagoMembresia = (bool)lector["pagomembresia"],
                            telefonoCliente = (string)lector["telefonoCliente"]
                        };
                        list.Add(aux);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            return list;
        }

        public List<Memberships> LoadAllMembershipData()
        {
            List<Memberships> list = new List<Memberships>();

            using (SqlConnection conexion = GetConnection())
            {
                SqlCommand comando = new SqlCommand(@"
                    SELECT c.clienteId, c.nombreCliente, c.apellidoCliente, m.membresiaId, m.inicioMembresia, m.finMembresia, m.vencidoMembresia, m.tipoMembresiaId, m.pagoMembresia
                    FROM dbo.Clientes c
                    JOIN dbo.Membresias m ON c.clienteId = m.clienteId;", conexion);

                try
                {
                    conexion.Open();
                    SqlDataReader lector = comando.ExecuteReader();

                    while (lector.Read())
                    {
                        Memberships aux = new Memberships
                        {
                            clienteId = (int)lector["clienteId"],
                            nombreCliente = (string)lector["nombreCliente"],
                            membresiaId = (int)lector["membresiaId"],
                            inicioMembresia = (DateTime)lector["inicioMembresia"],
                            finMembresia = (DateTime)lector["finMembresia"],
                            vencidoMembresia = (bool)lector["vencidoMembresia"],
                            tipoMembresiaId = (int)lector["tipoMembresiaId"],
                            pagoMembresia = (bool)lector["pagomembresia"]
                        };
                        list.Add(aux);
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    conexion.Close();
                }
            }

            return list;
        }
        public List<Ingresos> GetIncomesByCustomer(Member member)
        {
            List<Ingresos> list = new List<Ingresos>();
            DataAccess datos = new DataAccess();
            try
            {
                datos.setearConsulta("SELECT ingresoId, clienteId, transaccionId, fechaIngreso, cantidad, monto, tipoMembresiaId, saldoEstado FROM Ingresos WHERE clienteId = @clienteId");
                datos.setearParametro("@clienteId", member.clienteId);

                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Ingresos aux = new Ingresos
                    {
                        ingresoId = (int)datos.Lector["ingresoId"],
                        clienteId = (int)datos.Lector["clienteId"],
                        transaccionId = (int)datos.Lector["transaccionId"],
                        fechaIngreso = (DateTime)datos.Lector["fechaIngreso"],
                        cantidad = (int)datos.Lector["cantidad"],
                        monto = datos.Lector.GetDecimal(datos.Lector.GetOrdinal("monto")),
                        tipoMembresiaId = (int)datos.Lector["tipoMembresiaId"],
                        saldoEstado = datos.Lector.GetDecimal(datos.Lector.GetOrdinal("saldoEstado"))
                    };
                    list.Add(aux);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving incomes by customer", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
            return list;
        }

     

        public int GetLatestMember()
        {
            DataAccess datos = new DataAccess();
            datos.setearConsulta("SELECT MAX(clienteId) AS ultimoClienteId FROM Clientes;");
            datos.ejecutarLectura();

            int ultimoClienteId = 0;

            if (datos.Lector.Read() && !datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ultimoClienteId")))
            {
                string valor = datos.Lector["ultimoClienteId"].ToString();
                if (int.TryParse(valor, out int clienteId))
                {
                    ultimoClienteId = clienteId;
                }
                else
                {
                    Console.WriteLine($"No se pudo convertir {valor} a un entero.");
                }
            }

            datos.cerrarConexion();
            return ultimoClienteId;
        }
        public int GetLastTran()
        {
            DataAccess datos = new DataAccess();
            datos.setearConsulta("SELECT MAX(transaccionId) AS ultimaTranId FROM Transaccion;");
            datos.ejecutarLectura();
            int ultimaTranId = 0;
            if (datos.Lector.Read() && !datos.Lector.IsDBNull(datos.Lector.GetOrdinal("ultimaTranId")))
            {
                string valor = datos.Lector["ultimaTranId"].ToString();
                if (int.TryParse(valor, out int transaccionId))
                {
                    ultimaTranId = transaccionId;
                }
                else
                {
                    Console.WriteLine($"No se pudo convertir {valor} a un entero.");
                }
            }
            datos.cerrarConexion();
            return ultimaTranId;
        }
        public decimal GetLatestBalance()
        {
            DataAccess datos = new DataAccess();
            decimal latestBalance = 0;

            try

            {

                datos.setearConsulta("SELECT MAX(saldoEstado) AS saldoEstado FROM Ingresos");
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    if (!datos.Lector.IsDBNull(datos.Lector.GetOrdinal("saldoEstado")))
                    {
                        object saldoEstadoObj = datos.Lector["saldoEstado"];
                        if (saldoEstadoObj != null && decimal.TryParse(saldoEstadoObj.ToString(), out decimal saldoEstado))
                        {
                            latestBalance = saldoEstado;

                        }
                        else
                        {
                            throw new InvalidCastException("The saldoEstado field is not a decimal.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving latest balance: {ex.Message}", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }

            return latestBalance;
        }

        public int GetActiveMembersNum()
        {
            DataAccess datos = new DataAccess();
            int activeMembers = 0;
            try
            {
                datos.setearConsulta(" SELECT COUNT(DISTINCT c.clienteId) FROM dbo.Clientes c  JOIN dbo.Membresias m ON c.clienteId = m.clienteId  WHERE m.pagoMembresia = 1 AND m.vencidoMembresia = 0;");
                datos.ejecutarLectura();
                if (datos.Lector.Read())
                {
                    activeMembers = (int)datos.ejecutarEscalar();
                }
                return activeMembers;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving latest balance: {ex.Message}", ex);
            }
            finally
            {
                datos.cerrarConexion();
            }




        }



        #endregion

        #region INSERT Methods
        public void AddNewMember(Member nuevo)
        {
            using (DataAccess datos = new DataAccess())
            {
                try
                {
                    datos.setearConsulta("INSERT INTO Clientes (nombreCliente, apellidoCliente, dniCliente, nacimientoCliente, pesoCliente, alturaCliente, generoCliente, telefonoCliente, emailCliente) VALUES (@nombreCliente, @apellidoCliente, @dniCliente, @nacimientoCliente, @pesoCliente, @alturaCliente, @generoCliente, @telefonoCliente, @emailCliente)");

                    datos.setearParametro("@nombreCliente", nuevo.nombreCliente);
                    datos.setearParametro("@apellidoCliente", nuevo.apellidoCliente);
                    datos.setearParametro("@dniCliente", nuevo.dniCliente);
                    datos.setearParametro("@nacimientoCliente", nuevo.nacimientoCliente);
                    datos.setearParametro("@pesoCliente", nuevo.pesoCliente);
                    datos.setearParametro("@alturaCliente", nuevo.alturaCliente);
                    datos.setearParametro("@generoCliente", nuevo.generoCliente);
                    datos.setearParametro("@telefonoCliente", nuevo.telefonoCliente);
                    datos.setearParametro("@emailCliente", nuevo.emailCliente);
                    datos.ejecutarAccion();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }
        public void AddNewTransfer(Transaccion nuevo)
        {
            using (DataAccess datos = new DataAccess())
            {
                try
                {
                    datos.setearConsulta("INSERT INTO Transaccion (fechaTransaccion, metodoPago) VALUES (@fechaTransaccion, @metodoPago)");
                    datos.setearParametro("@fechaTransaccion", nuevo.fechaTransaccion);
                    datos.setearParametro("@metodoPago", nuevo.metodoPago);
                    datos.ejecutarAccion();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }
        public void AddNewIncome(Ingresos ingresos)
        {
            using (DataAccess datos = new DataAccess())
            {
                try
                {
                    datos.setearConsulta("INSERT INTO Ingresos (clienteId, transaccionId, fechaIngreso, saldoEstado, cantidad, monto, tipoMembresiaId) VALUES (@clienteId, @transaccionId, @fechaIngreso, @saldoEstado, @cantidad, @monto, @tipoMembresiaId);");

                    datos.setearParametro("@clienteId", ingresos.clienteId);
                    datos.setearParametro("@transaccionId", ingresos.transaccionId);
                    datos.setearParametro("@fechaIngreso", ingresos.fechaIngreso);
                    datos.setearParametro("@saldoEstado", ingresos.saldoEstado);
                    datos.setearParametro("@cantidad", ingresos.cantidad);
                    datos.setearParametro("@monto", ingresos.monto);
                    datos.setearParametro("@tipoMembresiaId", ingresos.tipoMembresiaId);

                    datos.ejecutarAccion();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }
        public void AddNewMembership(Memberships memberships)
        {
            using (DataAccess membership = new DataAccess())
            {
                try
                {
                    membership.setearConsulta("INSERT INTO Membresias (clienteId, inicioMembresia, finMembresia, vencidoMembresia, tipoMembresiaId, pagoMembresia) VALUES (@clienteId, @inicioMembresia, @finMembresia, @vencidoMembresia, @tipoMembresiaId, @pagoMembresia)");

                    membership.setearParametro("@clienteId", memberships.clienteId);
                    membership.setearParametro("@inicioMembresia", memberships.inicioMembresia);
                    membership.setearParametro("@finMembresia", memberships.finMembresia);
                    membership.setearParametro("@vencidoMembresia", memberships.vencidoMembresia);
                    membership.setearParametro("@tipoMembresiaId", memberships.tipoMembresiaId);
                    membership.setearParametro("@pagoMembresia", memberships.pagoMembresia);
                    membership.ejecutarAccion();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    membership.cerrarConexion();
                }
            }
        }
        #endregion

        #region UPDATE Methods
        public void UpdateMember(Member nuevo)
        {
            using (DataAccess datos = new DataAccess())
            {
                try
                {
                    datos.setearConsulta("UPDATE Clientes SET nombreCliente = @nombreCliente, apellidoCliente = @apellidoCliente, dniCliente = @dniCliente, nacimientoCliente = @nacimientoCliente, pesoCliente = @pesoCliente, alturaCliente = @alturaCliente, generoCliente = @generoCliente, telefonoCliente = @telefonoCliente, emailCliente = @emailCliente WHERE clienteId = @clienteId");

                    //set parameters 
                    datos.setearParametro("@nombreCliente", nuevo.nombreCliente);
                    datos.setearParametro("@apellidoCliente", nuevo.apellidoCliente);
                    datos.setearParametro("@dniCliente", nuevo.dniCliente);
                    datos.setearParametro("@nacimientoCliente", nuevo.nacimientoCliente);
                    datos.setearParametro("@pesoCliente", nuevo.pesoCliente);
                    datos.setearParametro("@alturaCliente", nuevo.alturaCliente);
                    datos.setearParametro("@generoCliente", nuevo.generoCliente);
                    datos.setearParametro("@telefonoCliente", nuevo.telefonoCliente);
                    datos.setearParametro("@emailCliente", nuevo.emailCliente);
                    datos.setearParametro("@clienteId", nuevo.clienteId);

                    // Execute action
                    datos.ejecutarAccion();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }

        public void LogicDelete(int clienteId)
        {
            try
            {
                using (DataAccess datos = new DataAccess())
                {
                    datos.setearConsulta("UPDATE Membresias SET vencidoMembresia = 1 WHERE clienteId = @idCliente");
                    datos.setearParametro("@idCliente", clienteId);
                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        public void UpdateSubscriptionStatus()
        {
            List<Memberships> membershipList = LoadAllMembershipData();
            using (DataAccess updateStatus = new DataAccess())
            {
                foreach (var membership in membershipList)
                {
                    try
                    {

                        DateTime scheduledEndDate = membership.finMembresia.AddDays(7);
                        bool isScheduled = DateTime.Now <= scheduledEndDate && DateTime.Now > membership.finMembresia;
                        bool isExpired;
                        bool isPaid = membership.pagoMembresia;
                        if (!isPaid && DateTime.Now > scheduledEndDate)
                        {
                            isExpired = true;
                        }
                        else
                        {
                            isExpired = false; // Ensure isExpired is initialized in all code paths
                        }



                        // Mostrar valores de depuración
                        //MessageBox.Show($"ClienteId: {membership.clienteId}, MembresiaId: {membership.membresiaId}, " +
                        //                $"FinMembresia: {membership.finMembresia}, IsExpired: {isExpired}, IsPaid: {isPaid}");

                        //string estadoPago = isExpired ? (isPaid ? "En Termino" : "Vencido") : (isPaid ? "En Termino" : "En Espera");

                        //// Mostrar estado de pago
                        //MessageBox.Show($"EstadoPago: {estadoPago}");

                        string updateQuery = @"
                                                UPDATE Membresias 
                                                SET vencidoMembresia = @VencidoMembresia,
                                                    pagoMembresia = @EstadoPago,
                                                    pagoPendienteMembresia = @PagoProgramado
                                                    
                                                WHERE membresiaId = @MembresiaId 
                                                  AND clienteId = @ClienteId;";

                        updateStatus.setearConsulta(updateQuery);

                        //SET PARAMETERS
                        updateStatus.setearParametro("@VencidoMembresia", isExpired ? 1 : 0);
                        updateStatus.setearParametro("@EstadoPago", isPaid ? 1 : 0);
                        updateStatus.setearParametro("@MembresiaId", membership.membresiaId);
                        updateStatus.setearParametro("@ClienteId", membership.clienteId);
                        updateStatus.setearParametro("@PagoProgramado", isScheduled ? 1 : 0);
                        // updateStatus.setearParametro("PagoPendiente"), 



                        //MessageBox.Show($"Actualizando MembresiaId: {membership.membresiaId}, ClienteId: {membership.clienteId}, VencidoMembresia: {isExpired}, EstadoPago: {estadoPago}");

                        //EXECUTE QUERY
                        updateStatus.ejecutarAccion();

                        // CLEAN PARAMETERS
                        updateStatus.limpiarParametros();
                    }
                    catch (SqlException sqlEx)
                    {
                        MessageBox.Show("SQL Error: " + sqlEx.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                    finally
                    {
                        updateStatus.cerrarConexion();
                    }
                }
            }
        }

        public void UpdateMembershipToPaid(Memberships membership)
        {
            List<Memberships> memberships = LoadAllMembershipData();
            using (DataAccess datos = new DataAccess())
            {
                try
                {

                    datos.setearConsulta("UPDATE Membresias SET pagoMembresia = @pagoMembresia, inicioMembresia = @inicioMembresia, finMembresia = @finMembresia, vencidoMembresia = @vencidoMembresia, tipoMembresiaId = @tipoMembresiaId,  pagoProgramadoMembresia = @pagoProgramadoMembresia, pagoPendienteMembresia = @pagoPendienteMembresia,pagoRechazadoMembresia = @pagoRechazadoMembresia   WHERE clienteId = @clienteId");
                    datos.setearParametro("@pagoMembresia", membership.pagoMembresia);
                    datos.setearParametro("@clienteId", membership.clienteId);
                    datos.setearParametro("@inicioMembresia", membership.inicioMembresia);
                    datos.setearParametro("@finMembresia", membership.finMembresia);
                    datos.setearParametro("@vencidoMembresia", membership.vencidoMembresia);
                    datos.setearParametro("@tipoMembresiaId", membership.tipoMembresiaId);
                    datos.setearParametro("@pagoProgramadoMembresia", membership.pagoProgramadoMembresia);
                    datos.setearParametro("@pagoPendienteMembresia", membership.pagoPendienteMembresia);
                    datos.setearParametro("@pagoRechazadoMembresia", membership.pagoRechazadoMembresia);

                    datos.ejecutarAccion();
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("SQL Error: " + sqlEx.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }
        #endregion



        #region Analysis
        private void GetOrderAnalisys()
        {
            GrossRevenueList = new List<RevenueByDate>();
            TotalProfit = 0;
            TotalRevenue = 0;
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"SELECT fechaIngreso, SUM(monto)
                            FROM [Ingresos]
                            WHERE fechaIngreso BETWEEN @fromDate AND @toDate
                            GROUP BY fechaIngreso";
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;

                    var resultTable = new List<KeyValuePair<DateTime, decimal>>();

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var fechaIngreso = reader.IsDBNull(0) ? DateTime.MinValue : reader.GetDateTime(0);
                            var monto = reader.IsDBNull(1) ? 0m : reader.GetDecimal(1);

                            resultTable.Add(new KeyValuePair<DateTime, decimal>(fechaIngreso, monto));
                            TotalRevenue += monto;
                        }
                    }
                    if (numberDays <= 1)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("hh tt")
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Days
                    else if (numberDays <= 30)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("dd MMM")
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Weeks
                    else if (numberDays <= 92)
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(
                                                orderList.Key, CalendarWeekRule.FirstDay, DayOfWeek.Monday)
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = "Week " + order.Key.ToString(),
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Months
                    else if (numberDays <= (365 * 2))
                    {
                        bool isYear = numberDays <= 365 ? true : false;
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("MMM yyyy")
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = isYear ? order.Key.Substring(0, order.Key.IndexOf(" ")) : order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                    //Group by Years
                    else
                    {
                        GrossRevenueList = (from orderList in resultTable
                                            group orderList by orderList.Key.ToString("yyyy")
                                           into order
                                            select new RevenueByDate
                                            {
                                                Date = order.Key,
                                                TotalAmount = order.Sum(amount => amount.Value)
                                            }).ToList();
                    }
                }
            }
        }

        private void GetProductAnalisys()
        {
            TopProductsList = new List<KeyValuePair<string, int>>();
            UnderstockList = new List<KeyValuePair<string, string>>();
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    SqlDataReader reader;
                    command.Connection = connection;
                    //Get Top 5 products
                    command.CommandText = @"
                                        SELECT TOP 5
                                            FORMAT(fechaAsistencia, 'hh tt') AS HOUR,
                                            COUNT(*) AS assistanceNumber
                                        FROM Asistencia
                                        WHERE presenteCheck = 1 
                                        AND fechaAsistencia BETWEEN @fromDate AND @toDate
                                        GROUP BY FORMAT(fechaAsistencia, 'hh tt')
                                        ORDER BY 
                                            assistanceNumber DESC;
                                    ";
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        TopProductsList.Add(
                            new KeyValuePair<string, int>(reader[0].ToString(), (int)reader[1]));
                        string hour = reader.GetString(0);
                        int assistanceNumber = reader.GetInt32(1);
                    }

                    reader.Close();
                    //Get Latest visitors
                    command.CommandText = @"SELECT c.nombreCliente, FORMAT(a.fechaAsistencia, 'hh tt') AS HOUR, MAX(a.fechaAsistencia) AS MaxFecha
                        FROM Asistencia A
                        JOIN Clientes C ON A.clienteId = C.clienteId
                        WHERE presenteCheck = 1
                          AND a.fechaAsistencia BETWEEN @fromDate AND @toDate
                        GROUP BY c.nombreCliente, FORMAT(a.fechaAsistencia, 'hh tt')
                        ORDER BY MaxFecha DESC;";
                    //          command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    //            command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        UnderstockList.Add(
                            new KeyValuePair<string, string>(reader[0].ToString(), reader.GetString(1))
                        );
                    }
                    reader.Close();

                }
            }
        }

        private void GetNumberItems()
        {
            using (var connection = GetConnection())
            {
                connection.Open();
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;

                    //Get Total Number of Visitors by date
                    command.CommandText = "SELECT COUNT(asistenciaId) FROM Asistencia WHERE fechaAsistencia BETWEEN @fromDate AND @toDate;";
                    command.Parameters.Clear();
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;
                    TotalVisitors = Convert.ToInt32(command.ExecuteScalar());

                    // Get Total Number of Customers
                    command.CommandText = "SELECT MAX(clienteId) FROM Clientes";
                    MembersNum = Convert.ToInt32(command.ExecuteScalar());

                    // Get Total Number of Transactions
                    command.CommandText = "SELECT COUNT(transaccionId) FROM Transaccion";
                    TranNumber = Convert.ToInt32(command.ExecuteScalar());

                    // TODO 
                    //command.CommandText = "SELECT COUNT(id) FROM Product";
                    //NumProducts = Convert.ToInt32(command.ExecuteScalar());

                    // Get Total Income in the date range
                    command.CommandText = @"
                                SELECT SUM (monto)
                                FROM Ingresos 
                                WHERE fechaIngreso BETWEEN @fromDate AND @toDate;";

                    // Clear parameters before adding new ones
                    command.Parameters.Clear();
                    command.Parameters.Add("@fromDate", System.Data.SqlDbType.DateTime).Value = startDate;
                    command.Parameters.Add("@toDate", System.Data.SqlDbType.DateTime).Value = endDate;

                    object result = command.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        Income = Convert.ToDecimal(result);
                    }
                    else
                    {
                        Income = 0; //conver to dec ex!
                    }


                    connection.Close();
                }

            }
        }

        public List<MemberAttendance> GetMemberAttendance(int clienteId, DateTime startDate, DateTime endDate)
        {
            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day, endDate.Hour, endDate.Minute, 59);
            
            var memberAttendanceList = new List<MemberAttendance>();
            if (startDate != this.fromDate || endDate != this.toDate)
            {
                DataAccess datos = new DataAccess();
                try
                {
                    datos.setearConsulta("SELECT fechaAsistencia, COUNT(CASE WHEN presenteCheck = '1' THEN 1 END) AS Attendance FROM Asistencia WHERE clienteId = @clienteId AND fechaAsistencia BETWEEN @startDate AND @endDate GROUP BY fechaAsistencia;");
                   datos.limpiarParametros();
                    datos.setearParametro("@clienteId", clienteId);
                    datos.setearParametro("@startDate", startDate);
                    datos.setearParametro("@endDate", endDate);
                    datos.ejecutarLectura();

                    while (datos.Lector.Read())
                    {
                        var fecha = datos.Lector.IsDBNull(0) ? DateTime.MinValue : datos.Lector.GetDateTime(0);
                        var cantidad = datos.Lector.IsDBNull(1) ? 0 : datos.Lector.GetInt32(1);

                        memberAttendanceList.Add(new MemberAttendance
                        {
                            Date = fecha.ToString("yyyy-MM-dd"),
                            Quantity = cantidad
                        });
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    throw;
                }

                this.startDate = startDate;
                this.endDate = endDate;
            }
            else
            {
                MessageBox.Show("Data not refreshed, same query: {0} - {1}", startDate.ToString());
            }

            return memberAttendanceList;
        }

        public bool LoadData(DateTime startDate, DateTime endDate)
        {

            endDate = new DateTime(endDate.Year, endDate.Month, endDate.Day,
                endDate.Hour, endDate.Minute, 59);
            if (startDate != this.startDate || endDate != this.endDate)
            {
                this.startDate = startDate;
                this.endDate = endDate;
                this.numberDays = (endDate - startDate).Days;
                GetNumberItems();
                GetProductAnalisys();
                GetOrderAnalisys();
                Console.WriteLine("Refreshed data: {0} - {1}", startDate.ToString(), endDate.ToString());
                return true;
            }
            else
            {
                Console.WriteLine("Data not refreshed, same query: {0} - {1}", startDate.ToString(), endDate.ToString());
                return false;
            }
        }
        public List<Member> filtrar(string Active, string Gender)
        {
            List<Member> lista = new List<Member>();
            using (DataAccess datos = new DataAccess())
            {
                try
                {
                    string consulta = "SELECT c.clienteId, c.nombreCliente, c.apellidoCliente, c.dniCliente, c.nacimientoCliente, c.pesoCliente, c.alturaCliente, c.generoCliente, c.telefonoCliente, c.emailCliente " +
                                      "FROM dbo.Clientes AS c " +
                                      "INNER JOIN dbo.Membresias AS m ON c.clienteId = m.clienteId";

                    bool whereAdded = false;

                    if (Active == "Active")
                    {
                        if (!whereAdded)
                        {
                            consulta += " WHERE ";
                            whereAdded = true;
                        }
                        switch (Active)
                        {
                            case "Active":
                                consulta += "m.vencidoMembresia = 0";
                                break;
                            case "Innactive":
                                consulta += "m.vencidoMembresia = 1";
                                break;
                            default:
                                throw new ArgumentException("Criterio no válido para el campo 'Active'");
                        }
                    }

                    if (Gender == "Gender")
                    {
                        if (!whereAdded)
                        {
                            consulta += " WHERE ";
                            whereAdded = true;
                        }
                        switch (Gender)
                        {
                            case "Male":
                            case "Female":
                            case "Non-specified":
                                consulta += "c.generoCliente = '" + Gender + "'";
                                break;
                            default:
                                throw new ArgumentException("Criterio no válido para el campo 'Gender'");
                        }
                    }

                    datos.setearConsulta(consulta);
                    datos.ejecutarLectura();
                    while (datos.Lector.Read())
                    {
                        Member prod = new Member();
                        prod.clienteId = (int)datos.Lector["clienteId"];
                        prod.nombreCliente = (string)datos.Lector["nombreCliente"];
                        prod.apellidoCliente = (string)datos.Lector["apellidoCliente"];
                        prod.dniCliente = (string)datos.Lector["dniCliente"];
                        prod.nacimientoCliente = (DateTime)datos.Lector["nacimientoCliente"];
                        prod.pesoCliente = (int)datos.Lector["pesoCliente"];
                        prod.alturaCliente = (decimal)datos.Lector["alturaCliente"];
                        prod.generoCliente = (string)datos.Lector["generoCliente"];
                        prod.telefonoCliente = (string)datos.Lector["telefonoCliente"];
                        prod.emailCliente = (string)datos.Lector["emailCliente"];

                        lista.Add(prod);
                    }

                    return lista;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return new List<Member>();
                }
                finally
                {
                    datos.cerrarConexion();
                }
            }
        }
        #endregion
    }
}
