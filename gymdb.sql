USE [emmanueltest]
GO

/****** Object:  Table [dbo].[Asistencia]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Asistencia](
	[asistenciaId] [int] IDENTITY(1,1) NOT NULL,
	[fechaAsistencia] [datetime] NOT NULL,
	[presenteCheck] [bit] NOT NULL,
	[clienteId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[asistenciaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Cliente]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Clientes]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clientes](
	[clienteId] [int] IDENTITY(1,1) NOT NULL,
	[nombreCliente] [nvarchar](20) NULL,
	[apellidoCliente] [nvarchar](20) NULL,
	[dniCliente] [nvarchar](20) NULL,
	[nacimientoCliente] [date] NULL,
	[pesoCliente] [int] NOT NULL,
	[alturaCliente] [decimal](18, 0) NOT NULL,
	[generoCliente] [nvarchar](10) NULL,
	[telefonoCliente] [nvarchar](20) NULL,
	[emailCliente] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[clienteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Departamentos]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Departamentos](
	[departamentoId] [int] IDENTITY(1,1) NOT NULL,
	[nombreDepartamento] [nvarchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[departamentoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Egresos]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Egresos](
	[egresoId] [int] IDENTITY(1,1) NOT NULL,
	[transaccionId] [int] NOT NULL,
	[egreso] [decimal](10, 2) NOT NULL,
	[descripcion] [nvarchar](60) NOT NULL,
	[saldoActual] [decimal](10, 2) NULL,
	[sueldoId] [int] NULL,
	[saldoAnterior] [decimal](10, 2) NULL,
	[fechaEgreso] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[egresoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Empleado]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Empleado](
	[empleadoId] [int] IDENTITY(1,1) NOT NULL,
	[nombreEmpleado] [nvarchar](20) NULL,
	[apellidoEmpleado] [nvarchar](20) NULL,
	[dniEmpleado] [nvarchar](20) NULL,
	[nacimientoEmpleado] [date] NULL,
	[contratacionEmpleado] [date] NULL,
	[generoEmpleado] [nvarchar](10) NULL,
	[telefonoEmpleado] [nvarchar](20) NULL,
	[emailEmpleado] [nvarchar](50) NULL,
	[puestoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[empleadoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO



/****** Object:  Table [dbo].[FichaMedica]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[FichaMedica](
	[fichaId] [int] IDENTITY(1,1) NOT NULL,
	[clienteId] [int] NOT NULL,
	[fumador] [bit] NULL,
	[cardiopatias] [bit] NULL,
	[respiratorios] [bit] NULL,
	[convulsiones] [bit] NULL,
	[diabetes] [bit] NULL,
	[alteracionesSanguineas] [bit] NULL,
	[afeccionesAuditivas] [bit] NULL,
	[cirujias] [bit] NULL,
	[alergias] [bit] NULL,
	[fracturas] [bit] NULL,
	[vitaminas] [bit] NULL,
	[colesterol] [bit] NULL,
	[obesidad] [bit] NULL,
	[fichaObservacion] [nvarchar](250) NULL,
PRIMARY KEY CLUSTERED 
(
	[fichaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[HistorialRevision]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HistorialRevision](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EspecificacionId] [int] NULL,
	[Revision] [int] NULL,
	[FechaRevision] [date] NULL,
	[UltimaModificacion] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Ingresos]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ingresos](
	[ingresoId] [int] IDENTITY(1,1) NOT NULL,
	[clienteId] [int] NOT NULL,
	[transaccionId] [int] NOT NULL,
	[fechaIngreso] [datetime] NOT NULL,
	[cantidad] [int] NULL,
	[monto] [decimal](10, 2) NULL,
	[tipoMembresiaId] [int] NULL,
	[saldoEstado] [decimal](10, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[ingresoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[LineasEgresos]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LineasEgresos](
	[lineaEgresoId] [int] IDENTITY(1,1) NOT NULL,
	[egresoId] [int] NOT NULL,
	[cantidad] [int] NOT NULL,
	[precio] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[lineaEgresoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Mediciones]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Mediciones](
	[medicionId] [int] IDENTITY(1,1) NOT NULL,
	[fechaMedicion] [datetime] NOT NULL,
	[alturaMedicion] [float] NULL,
	[pesoMedicion] [float] NULL,
	[imcMedicion] [float] NULL,
	[brazoMedicion] [float] NULL,
	[grasaMedicion] [float] NULL,
	[gvMedicion] [float] NULL,
	[observacionesMedicion] [nvarchar](250) NULL,
	[clienteId] [int] NOT NULL,
	[empleadoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[medicionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Membresias]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Membresias](
	[membresiaId] [int] IDENTITY(1,1) NOT NULL,
	[inicioMembresia] [date] NOT NULL,
	[finMembresia] [date] NOT NULL,
	[vencidoMembresia] [bit] NOT NULL,
	[clienteId] [int] NOT NULL,
	[tipoMembresiaId] [int] NOT NULL,
	[pagoMembresia] [bit] NULL,
	[pagoProgramadoMembresia] [bit] NULL,
	[pagoPendienteMembresia] [bit] NULL,
	[pagoRechazadoMembresia] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[membresiaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[MembresiasTipo]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MembresiasTipo](
	[tipoMembresiaId] [int] IDENTITY(1,1) NOT NULL,
	[membresiaTipo] [nvarchar](50) NOT NULL,
	[membresiaCosto] [decimal](18, 0) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tipoMembresiaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PlanEntrenamiento]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PlanEntrenamiento](
	[entrenamientoId] [int] IDENTITY(1,1) NOT NULL,
	[empleadoId] [int] NOT NULL,
	[clienteId] [int] NOT NULL,
	[planEntrenamiento] [nvarchar](50) NULL,
	[observacionesEntrenamiento] [nvarchar](250) NULL,
	[inicioEntrenamiento] [date] NULL,
	[finEntrenamiento] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[entrenamientoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


/****** Object:  Table [dbo].[Puestos]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Puestos](
	[puestoId] [int] IDENTITY(1,1) NOT NULL,
	[nombrePuesto] [nvarchar](50) NOT NULL,
	[departamentoId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[puestoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Sueldo]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sueldo](
	[sueldoId] [int] IDENTITY(1,1) NOT NULL,
	[empleadoId] [int] NOT NULL,
	[salario] [decimal](18, 0) NOT NULL,
	[situacionTributaria] [nvarchar](100) NULL,
	[fechaPago] [date] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[sueldoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Tarifas]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tarifas](
	[tarifaId] [int] IDENTITY(1,1) NOT NULL,
	[membresiaId] [int] NOT NULL,
	[tarifa] [nvarchar](60) NOT NULL,
	[estado] [char](1) NOT NULL,
	[descripcion] [nvarchar](250) NULL,
	[precio] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[tarifaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Transaccion]    Script Date: 8/5/2024 8:31:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Transaccion](
	[transaccionId] [int] IDENTITY(1,1) NOT NULL,
	[fechaTransaccion] [datetime] NOT NULL,
	[metodoPago] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[transaccionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Membresias] ADD  DEFAULT ((0)) FOR [pagoProgramadoMembresia]
GO

ALTER TABLE [dbo].[Membresias] ADD  DEFAULT ((0)) FOR [pagoPendienteMembresia]
GO

ALTER TABLE [dbo].[Membresias] ADD  DEFAULT ((0)) FOR [pagoRechazadoMembresia]
GO

ALTER TABLE [dbo].[Asistencia]  WITH CHECK ADD  CONSTRAINT [FK_asistenciaCliente] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Clientes] ([clienteId])
GO

ALTER TABLE [dbo].[Asistencia] CHECK CONSTRAINT [FK_asistenciaCliente]
GO

ALTER TABLE [dbo].[Egresos]  WITH CHECK ADD  CONSTRAINT [FK_sueldoEgreso] FOREIGN KEY([sueldoId])
REFERENCES [dbo].[Sueldo] ([sueldoId])
GO

ALTER TABLE [dbo].[Egresos] CHECK CONSTRAINT [FK_sueldoEgreso]
GO

ALTER TABLE [dbo].[Egresos]  WITH CHECK ADD  CONSTRAINT [FK_transaccionEgreso] FOREIGN KEY([transaccionId])
REFERENCES [dbo].[Transaccion] ([transaccionId])
GO

ALTER TABLE [dbo].[Egresos] CHECK CONSTRAINT [FK_transaccionEgreso]
GO

ALTER TABLE [dbo].[Empleado]  WITH CHECK ADD  CONSTRAINT [FK_puestoEmpleado] FOREIGN KEY([puestoId])
REFERENCES [dbo].[Puestos] ([puestoId])
GO

ALTER TABLE [dbo].[Empleado] CHECK CONSTRAINT [FK_puestoEmpleado]
GO

ALTER TABLE [dbo].[FichaMedica]  WITH CHECK ADD  CONSTRAINT [FK_clienteId_FichaMedica] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Clientes] ([clienteId])
GO

ALTER TABLE [dbo].[FichaMedica] CHECK CONSTRAINT [FK_clienteId_FichaMedica]
GO

ALTER TABLE [dbo].[HistorialRevision]  WITH CHECK ADD FOREIGN KEY([EspecificacionId])
REFERENCES [dbo].[EspecificacionTecnica] ([Id])
GO

ALTER TABLE [dbo].[Ingresos]  WITH CHECK ADD  CONSTRAINT [FK_clienteIngreso] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Clientes] ([clienteId])
GO

ALTER TABLE [dbo].[Ingresos] CHECK CONSTRAINT [FK_clienteIngreso]
GO

ALTER TABLE [dbo].[Ingresos]  WITH CHECK ADD  CONSTRAINT [fk_tipoMembresiaId] FOREIGN KEY([tipoMembresiaId])
REFERENCES [dbo].[MembresiasTipo] ([tipoMembresiaId])
GO

ALTER TABLE [dbo].[Ingresos] CHECK CONSTRAINT [fk_tipoMembresiaId]
GO

ALTER TABLE [dbo].[Ingresos]  WITH CHECK ADD  CONSTRAINT [FK_transaccionIngreso] FOREIGN KEY([transaccionId])
REFERENCES [dbo].[Transaccion] ([transaccionId])
GO

ALTER TABLE [dbo].[Ingresos] CHECK CONSTRAINT [FK_transaccionIngreso]
GO

ALTER TABLE [dbo].[LineasEgresos]  WITH CHECK ADD  CONSTRAINT [FK_egresoLinea] FOREIGN KEY([egresoId])
REFERENCES [dbo].[Egresos] ([egresoId])
GO

ALTER TABLE [dbo].[LineasEgresos] CHECK CONSTRAINT [FK_egresoLinea]
GO

ALTER TABLE [dbo].[Mediciones]  WITH CHECK ADD  CONSTRAINT [FK_clienteId_Mediciones] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Clientes] ([clienteId])
GO

ALTER TABLE [dbo].[Mediciones] CHECK CONSTRAINT [FK_clienteId_Mediciones]
GO

ALTER TABLE [dbo].[Mediciones]  WITH CHECK ADD  CONSTRAINT [FK_empleadoId_Mediciones] FOREIGN KEY([empleadoId])
REFERENCES [dbo].[Empleado] ([empleadoId])
GO

ALTER TABLE [dbo].[Mediciones] CHECK CONSTRAINT [FK_empleadoId_Mediciones]
GO

ALTER TABLE [dbo].[Membresias]  WITH CHECK ADD  CONSTRAINT [FK_membresiaCliente] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Clientes] ([clienteId])
GO

ALTER TABLE [dbo].[Membresias] CHECK CONSTRAINT [FK_membresiaCliente]
GO

ALTER TABLE [dbo].[Membresias]  WITH CHECK ADD  CONSTRAINT [FK_membresiaTipo] FOREIGN KEY([tipoMembresiaId])
REFERENCES [dbo].[MembresiasTipo] ([tipoMembresiaId])
GO

ALTER TABLE [dbo].[Membresias] CHECK CONSTRAINT [FK_membresiaTipo]
GO

ALTER TABLE [dbo].[PlanEntrenamiento]  WITH CHECK ADD  CONSTRAINT [FK_clienteId_PlanEntrenamiento] FOREIGN KEY([clienteId])
REFERENCES [dbo].[Clientes] ([clienteId])
GO

ALTER TABLE [dbo].[PlanEntrenamiento] CHECK CONSTRAINT [FK_clienteId_PlanEntrenamiento]
GO

ALTER TABLE [dbo].[PlanEntrenamiento]  WITH CHECK ADD  CONSTRAINT [FK_empleadoId_PlanEntrenamiento] FOREIGN KEY([empleadoId])
REFERENCES [dbo].[Empleado] ([empleadoId])
GO

ALTER TABLE [dbo].[PlanEntrenamiento] CHECK CONSTRAINT [FK_empleadoId_PlanEntrenamiento]
GO

ALTER TABLE [dbo].[Puestos]  WITH CHECK ADD  CONSTRAINT [FK_puestoDepartamento] FOREIGN KEY([departamentoId])
REFERENCES [dbo].[Departamentos] ([departamentoId])
GO

ALTER TABLE [dbo].[Puestos] CHECK CONSTRAINT [FK_puestoDepartamento]
GO

ALTER TABLE [dbo].[Sueldo]  WITH CHECK ADD  CONSTRAINT [FK_sueldoEmpleado] FOREIGN KEY([empleadoId])
REFERENCES [dbo].[Empleado] ([empleadoId])
GO

ALTER TABLE [dbo].[Sueldo] CHECK CONSTRAINT [FK_sueldoEmpleado]
GO

ALTER TABLE [dbo].[Tarifas]  WITH CHECK ADD  CONSTRAINT [FK_membresiaTarifa] FOREIGN KEY([membresiaId])
REFERENCES [dbo].[Membresias] ([membresiaId])
GO

ALTER TABLE [dbo].[Tarifas] CHECK CONSTRAINT [FK_membresiaTarifa]
GO

