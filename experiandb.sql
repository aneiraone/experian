-- phpMyAdmin SQL Dump
-- version 4.9.0.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 02-05-2022 a las 03:49:19
-- Versión del servidor: 10.4.6-MariaDB
-- Versión de PHP: 7.2.22

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET AUTOCOMMIT = 0;
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `experiandb`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `documento`
--

CREATE TABLE `documento` (
  `Id` int(11) NOT NULL,
  `Rut` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `Folio` int(11) NOT NULL,
  `TipoDocumento` int(11) NOT NULL,
  `Data` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `Estado` int(11) NOT NULL,
  `FechaModificacion` datetime(6) NOT NULL,
  `FechaCreacion` datetime(6) NOT NULL,
  `Error` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `Razon` longtext CHARACTER SET utf8mb4 DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `documento`
--

INSERT INTO `documento` (`Id`, `Rut`, `Folio`, `TipoDocumento`, `Data`, `Estado`, `FechaModificacion`, `FechaCreacion`, `Error`, `Razon`) VALUES
(1, '22222222-1', 222333, 33, '{\"Encabezado\":{\"TipoDocumento\":\"33\",\"FolioCliente\":\"222333\",\"FechaEmision\":\"2020-12-12\",\"IndicadorNoRebaja\":true,\"TipoDespacho\":1,\"IndicadorTraslado\":8,\"IndicadorServicioPeriodico\":3,\"IndicadorMontoBruto\":true,\"FormaPago\":2,\"FechaCancelacion\":\"2020-11-30\",\"PeriodoDesde\":\"2020-11-30\",\"PeriodoHasta\":\"2020-12-31\",\"MedioPago\":\"LT\",\"CodigoTerminoPago\":\"4544\",\"DiasTerminoPago\":30,\"FechaVencimiento\":\"2020-12-31\",\"Emisor\":{\"RUT\":\"78079790-8\",\"RazonSocial\":\"Esta es una pruebá\",\"Giro\":\"otro giro emis\",\"Sucursal\":\"sucursal prueba emis\",\"CodigoSucursalSII\":45454445,\"CodigoAdicionalSucursal\":\"DDSJHD42434\",\"IdentificadorAdicional\":\"44454454\",\"Ciudad\":\"santiago\",\"Comuna\":\"Santiago \'centro\",\"Acteco\":[\"726000\"],\"Direccion\":\"Una direccion emisora ahi\",\"CodigoVendedor\":\"23\",\"RUTMandante\":\"15455456-4\"},\"Receptor\":{\"RUT\":\"22222222-1\",\"RazonSocial\":\"Esta es una pruebá rec\",\"Giro\":\"otro girodd\",\"Ciudad\":\"santiagoasdfasdf\",\"Comuna\":\"Santiago centroasdf\",\"Direccion\":\"Una dirección \'receptora ahi\",\"DireccionPostal\":\"Una dirección postal de prueba receptora\",\"ComunaPostal\":\"Comuna postal recep\",\"RUTSolicitante\":\"45454545-4\",\"DatosReceptorExtranjero\":{\"NroIdentificador\":\"ADASD8797987987\",\"Nacionalidad\":\"VEN\",\"IdentificacionAdicional\":\"23423DSDD\"},\"Contacto\":\"Contacto de documento prueba\",\"CorreoContacto\":\"uncorreo@correito.cl\",\"CodigoInterno\":\"12312312DHHDSADKHSDK\"},\"Transporte\":{\"PatenteVehiculo\":\"DDFDASD\",\"RUTTransportista\":\"21412455-4\",\"DireccionDestino\":\"Una direccion receptora ahi \",\"ComunaDestino\":\"Comunita de destino\",\"CiudadDestino\":\"Ciudad destinooo\",\"Chofer\":{\"RUT\":\"12121212-1\",\"Nombre\":\"áéíóúChofer \"},\"Aduana\":{\"CodigoModalidadVenta\":1,\"CodigoClausulaVenta\":2,\"TotalClausulaVenta\":3,\"CodigoViaTransporte\":4,\"NombreTransporte\":\"NombreTransporte\",\"RutCompaniaTransporte\":\"55555555-5\",\"NombreCompaniaTransporte\":\"NombreCompaniaTransporte\",\"InformacionAdicionalTransporte\":\"InfAdicTransporte\",\"IdentificadorAdicionalPuertoEmbarque\":\"IdAdicPuertoEmbarque\",\"IdentificadorAdicionalPuertoDesembarque\":\"IdAdicPuertoDesemb\",\"MontoFlete\":7,\"MontoSeguro\":8,\"CodigoPaisReceptor\":9,\"CodigoPaisDestino\":10,\"Booking\":\"DDFASD65665\",\"CodigoOperador\":\"454545DDDD\",\"CodigoPuertoEmbarque\":1234,\"CodigoPuertoDesembarque\":6543,\"Tara\":465465,\"CodigoUnidadMedidaTara\":55,\"PesoBruto\":45445.25,\"CodigoUnidadMedidaPesoBruto\":54,\"PesoNeto\":45445.25,\"CodigoUnidadMedidaPesoNeto\":45,\"TotalItems\":4545545,\"TotalBultos\":798746516}},\"MontoTotal\":15777,\"MontoNeto\":123,\"MontoExento\":456,\"MontoBase\":1000,\"TasaIVA\":10,\"IVA\":1000,\"IVANoRetenido\":90123,\"CreditoEmpresaConstruccion\":45545,\"MontoPeriodo\":880000,\"GarantiaDeposito\":9032423,\"MontoNoFacturable\":1000,\"SaldoAnterior\":10000,\"ValorAPagar\":100000,\"TipoImpresion\":\"T\",\"MontoCancelado\":1000,\"SaldoInsoluto\":654555,\"FormaPagoExportacion\":10,\"NroCuentaPago\":\"123123123\",\"BancoPago\":\"Banco cualquiera\",\"GlosaTerminoPago\":\"Pago de prueba\",\"CodigoTraslado\":1,\"FolioAutorizado\":2244,\"FechaAutorizacion\":\"2020-11-20\",\"TipoMoneda\":\"BOLIVAR\",\"MontoMargenComercializacion\":45554,\"IVAPropio\":5666766,\"IVATerceros\":47488544,\"TipoCambio\":10.2545,\"MontoNetoOtraMoneda\":1111000.22,\"MontoExentoOtraMoneda\":455554.305,\"MontoFaenamientoCarnesOtraMoneda\":45454.33,\"MontoMargenComercializacionOtraMoneda\":40111.22,\"IVAOtraMoneda\":454545,\"IVANoRetenidoOtraMoneda\":25454.333333333,\"MontoTotalOtraMoneda\":1000000,\"TipoTransaccionVenta\":1,\"TipoTransaccionCompra\":1,\"TipoFacturaEspecial\":1,\"TipoIdentificadorDocumento\":1},\"ImpuestosYRetenciones\":[{\"Tipo\":\"23\",\"Tasa\":100,\"Valor\":411120},{\"Tipo\":\"45\",\"Tasa\":1,\"Valor\":41}],\"ImpuestosYRetencionesOtraMoneda\":[{\"Tipo\":\"17\",\"Tasa\":100,\"Valor\":411120},{\"Tipo\":\"52\",\"Tasa\":1,\"Valor\":41}],\"Detalles\":[{\"NroLinea\":1,\"Nombre\":\"Item de prueba\",\"PrecioUnitario\":10,\"Monto\":400,\"Cantidad\":40,\"IndicadorExencion\":4,\"CantidadReferencia\":1555.4454,\"UnidadMedidaReferencia\":\"cm\",\"PrecioReferencia\":11111.22,\"FechaElaboracion\":\"2020-11-04\",\"FechaVencimiento\":\"2021-11-01\",\"UnidadMedida\":\"METR\",\"PrecioUnitarioOtraMoneda\":12002.222,\"CodigoOtraMoneda\":\"CLP\",\"FactorConversionPesos\":810.2,\"PorcentajeDescuento\":10.45,\"MontoDescuento\":44000,\"PorcentajeRecargo\":15.22,\"MontoRecargo\":10000,\"CodigoImpuestoAdicional\":\"45\",\"DescuentoOtraMoneda\":1000.111,\"RecargoOtraMoneda\":102000.22,\"MontoOtraMoneda\":15454.23,\"IndicadorAgente\":\"R\",\"MontoBaseFaena\":10000,\"MargenComercializacion\":56600,\"PrecioUnitarioFinal\":40014,\"Subdescuentos\":[{\"Tipo\":\"%\",\"Valor\":15},{\"Tipo\":\"$\",\"Valor\":100000}],\"CodigosItem\":[{\"Tipo\":\"INT1\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT2\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT3\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT4\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT5\",\"Codigo\":\"ASD2341\"}],\"Subrecargos\":[{\"Tipo\":\"%\",\"Valor\":15},{\"Tipo\":\"$\",\"Valor\":100023}],\"Subcantidades\":[{\"CantidadDistribuida\":20.5,\"Codigo\":\"CODIGO1234\",\"TipoCodigo\":\"TIPOCODI\"}]},{\"NroLinea\":34,\"Nombre\":\"Item de prueba2\",\"PrecioUnitario\":10,\"Descripcion\":\"Lorem ipsum dolor sit cuchuflí barquillo bacán jote gamba listeilor po cahuín, luca melón con vino pichanga coscacho ni ahí peinar la muñeca chuchada al chancho achoclonar. Chorrocientos pituto ubicatex huevo duro bolsero cachureo el hoyo del queque en cana huevón el año del loly hacerla corta impeque de miedo quilterry la raja longi ñecla. Hilo curado rayuela carrete quina guagua lorea piola ni ahí.\",\"Monto\":120,\"Cantidad\":12,\"Subdescuentos\":[{\"Tipo\":\"%\",\"Valor\":15},{\"Tipo\":\"$\",\"Valor\":100000}],\"CodigosItem\":[{\"Tipo\":\"INT6\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT7\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT8\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT9\",\"Codigo\":\"ASD2341\"}]}],\"DescuentosYRecargosGlobales\":[{\"NroLinea\":1,\"TipoMovimiento\":\"R\",\"Glosa\":\"Lorem ipsum dolor sit cuchuflí barquillo \",\"TipoValor\":\"$\",\"Valor\":40001.111,\"IndicadorExcecion\":1,\"ValorOtraMoneda\":200000.336}],\"ReferenciasGlobales\":[{\"NroLinea\":1,\"TipoDocumento\":\"DOC\",\"IndicadorGlobal\":1,\"Folio\":\"FE9000001\",\"RUTOtroContribuyente\":\"11111111-1\",\"Fecha\":\"2020-11-25\",\"Codigo\":1,\"Razon\":\"Razon que desconozco\"}],\"ComisionesYOtrosCargos\":[{\"NroLinea\":1,\"TipoMovimiento\":\"C\",\"Glosa\":\"Lorem ipsum dolor sit cuchuflí barquillo \",\"Tasa\":15.111,\"ValorNeto\":1000,\"ValorExento\":10000,\"ValorIVA\":40004}],\"Acciones\":[{\"Codigo\":\"ALGO\",\"NumeroImpresiones\":10,\"LogoDTE\":\"DD45\",\"Plantilla\":\"dirplantilla\"}],\"MailEnvio\":[{\"NroLinea\":1,\"Direccion\":\"uncorreito@cualquiera.com\",\"Copia\":\"uncorreito@cualquiera.com\",\"CopiaOculta\":\"uncorreito@cualquiera.com\",\"Mensaje\":\"Lorem ipsum dolor sit cuchuflí barquillo bacán jote gamba listeilor po cahuín, luca melón con vino pichanga coscacho ni ahí peinar la muñeca chuchada al chancho achoclonar. Chorrocientos pituto ubicatex huevo duro bolsero cachureo el hoyo del queque en cana huevón el año del loly hacerla corta impeque de miedo quilterry la raja longi ñecla. Hilo curado rayuela carrete quina guagua lorea piola ni ahí.\"}],\"Adjuntos\":[{\"NroLinea\":1,\"Tipo\":\"DOC\",\"Ruta\":\"C:\\\\ruta\\\\aqui\\\\cualquiera\"},{\"NroLinea\":2,\"Tipo\":\"TXT\",\"Ruta\":\"C:\\\\ruta\\\\aqui\\\\cualquieraXML\"}],\"ValoresLibres\":[\"Impresora\",\"Hola\",\"esto\",\"e\'s\",\"un\",\"Valor\",\"li\'bre\",\"Por\",\"Si\",\"Acaso\"]}', 0, '2022-04-25 00:01:51.368417', '2022-04-25 00:01:51.368384', NULL, NULL),
(2, '22222222-1', 222333, 33, '{\"Encabezado\":{\"TipoDocumento\":\"33\",\"FolioCliente\":\"222333\",\"FechaEmision\":\"2020-12-12\",\"IndicadorNoRebaja\":true,\"TipoDespacho\":1,\"IndicadorTraslado\":8,\"IndicadorServicioPeriodico\":3,\"IndicadorMontoBruto\":true,\"FormaPago\":2,\"FechaCancelacion\":\"2020-11-30\",\"PeriodoDesde\":\"2020-11-30\",\"PeriodoHasta\":\"2020-12-31\",\"MedioPago\":\"LT\",\"CodigoTerminoPago\":\"4544\",\"DiasTerminoPago\":30,\"FechaVencimiento\":\"2020-12-31\",\"Emisor\":{\"RUT\":\"78079790-8\",\"RazonSocial\":\"Esta es una pruebá\",\"Giro\":\"otro giro emis\",\"Sucursal\":\"sucursal prueba emis\",\"CodigoSucursalSII\":45454445,\"CodigoAdicionalSucursal\":\"DDSJHD42434\",\"IdentificadorAdicional\":\"44454454\",\"Ciudad\":\"santiago\",\"Comuna\":\"Santiago \'centro\",\"Acteco\":[\"726000\"],\"Direccion\":\"Una direccion emisora ahi\",\"CodigoVendedor\":\"23\",\"RUTMandante\":\"15455456-4\"},\"Receptor\":{\"RUT\":\"22222222-1\",\"RazonSocial\":\"Esta es una pruebá rec\",\"Giro\":\"otro girodd\",\"Ciudad\":\"santiagoasdfasdf\",\"Comuna\":\"Santiago centroasdf\",\"Direccion\":\"Una dirección \'receptora ahi\",\"DireccionPostal\":\"Una dirección postal de prueba receptora\",\"ComunaPostal\":\"Comuna postal recep\",\"RUTSolicitante\":\"45454545-4\",\"DatosReceptorExtranjero\":{\"NroIdentificador\":\"ADASD8797987987\",\"Nacionalidad\":\"VEN\",\"IdentificacionAdicional\":\"23423DSDD\"},\"Contacto\":\"Contacto de documento prueba\",\"CorreoContacto\":\"uncorreo@correito.cl\",\"CodigoInterno\":\"12312312DHHDSADKHSDK\"},\"Transporte\":{\"PatenteVehiculo\":\"DDFDASD\",\"RUTTransportista\":\"21412455-4\",\"DireccionDestino\":\"Una direccion receptora ahi \",\"ComunaDestino\":\"Comunita de destino\",\"CiudadDestino\":\"Ciudad destinooo\",\"Chofer\":{\"RUT\":\"12121212-1\",\"Nombre\":\"áéíóúChofer \"},\"Aduana\":{\"CodigoModalidadVenta\":1,\"CodigoClausulaVenta\":2,\"TotalClausulaVenta\":3,\"CodigoViaTransporte\":4,\"NombreTransporte\":\"NombreTransporte\",\"RutCompaniaTransporte\":\"55555555-5\",\"NombreCompaniaTransporte\":\"NombreCompaniaTransporte\",\"InformacionAdicionalTransporte\":\"InfAdicTransporte\",\"IdentificadorAdicionalPuertoEmbarque\":\"IdAdicPuertoEmbarque\",\"IdentificadorAdicionalPuertoDesembarque\":\"IdAdicPuertoDesemb\",\"MontoFlete\":7,\"MontoSeguro\":8,\"CodigoPaisReceptor\":9,\"CodigoPaisDestino\":10,\"Booking\":\"DDFASD65665\",\"CodigoOperador\":\"454545DDDD\",\"CodigoPuertoEmbarque\":1234,\"CodigoPuertoDesembarque\":6543,\"Tara\":465465,\"CodigoUnidadMedidaTara\":55,\"PesoBruto\":45445.25,\"CodigoUnidadMedidaPesoBruto\":54,\"PesoNeto\":45445.25,\"CodigoUnidadMedidaPesoNeto\":45,\"TotalItems\":4545545,\"TotalBultos\":798746516}},\"MontoTotal\":15777,\"MontoNeto\":123,\"MontoExento\":456,\"MontoBase\":1000,\"TasaIVA\":10,\"IVA\":1000,\"IVANoRetenido\":90123,\"CreditoEmpresaConstruccion\":45545,\"MontoPeriodo\":880000,\"GarantiaDeposito\":9032423,\"MontoNoFacturable\":1000,\"SaldoAnterior\":10000,\"ValorAPagar\":100000,\"TipoImpresion\":\"T\",\"MontoCancelado\":1000,\"SaldoInsoluto\":654555,\"FormaPagoExportacion\":10,\"NroCuentaPago\":\"123123123\",\"BancoPago\":\"Banco cualquiera\",\"GlosaTerminoPago\":\"Pago de prueba\",\"CodigoTraslado\":1,\"FolioAutorizado\":2244,\"FechaAutorizacion\":\"2020-11-20\",\"TipoMoneda\":\"BOLIVAR\",\"MontoMargenComercializacion\":45554,\"IVAPropio\":5666766,\"IVATerceros\":47488544,\"TipoCambio\":10.2545,\"MontoNetoOtraMoneda\":1111000.22,\"MontoExentoOtraMoneda\":455554.305,\"MontoFaenamientoCarnesOtraMoneda\":45454.33,\"MontoMargenComercializacionOtraMoneda\":40111.22,\"IVAOtraMoneda\":454545,\"IVANoRetenidoOtraMoneda\":25454.333333333,\"MontoTotalOtraMoneda\":1000000,\"TipoTransaccionVenta\":1,\"TipoTransaccionCompra\":1,\"TipoFacturaEspecial\":1,\"TipoIdentificadorDocumento\":1},\"ImpuestosYRetenciones\":[{\"Tipo\":\"23\",\"Tasa\":100,\"Valor\":411120},{\"Tipo\":\"45\",\"Tasa\":1,\"Valor\":41}],\"ImpuestosYRetencionesOtraMoneda\":[{\"Tipo\":\"17\",\"Tasa\":100,\"Valor\":411120},{\"Tipo\":\"52\",\"Tasa\":1,\"Valor\":41}],\"Detalles\":[{\"NroLinea\":1,\"Nombre\":\"Item de prueba\",\"PrecioUnitario\":10,\"Monto\":400,\"Cantidad\":40,\"IndicadorExencion\":4,\"CantidadReferencia\":1555.4454,\"UnidadMedidaReferencia\":\"cm\",\"PrecioReferencia\":11111.22,\"FechaElaboracion\":\"2020-11-04\",\"FechaVencimiento\":\"2021-11-01\",\"UnidadMedida\":\"METR\",\"PrecioUnitarioOtraMoneda\":12002.222,\"CodigoOtraMoneda\":\"CLP\",\"FactorConversionPesos\":810.2,\"PorcentajeDescuento\":10.45,\"MontoDescuento\":44000,\"PorcentajeRecargo\":15.22,\"MontoRecargo\":10000,\"CodigoImpuestoAdicional\":\"45\",\"DescuentoOtraMoneda\":1000.111,\"RecargoOtraMoneda\":102000.22,\"MontoOtraMoneda\":15454.23,\"IndicadorAgente\":\"R\",\"MontoBaseFaena\":10000,\"MargenComercializacion\":56600,\"PrecioUnitarioFinal\":40014,\"Subdescuentos\":[{\"Tipo\":\"%\",\"Valor\":15},{\"Tipo\":\"$\",\"Valor\":100000}],\"CodigosItem\":[{\"Tipo\":\"INT1\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT2\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT3\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT4\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT5\",\"Codigo\":\"ASD2341\"}],\"Subrecargos\":[{\"Tipo\":\"%\",\"Valor\":15},{\"Tipo\":\"$\",\"Valor\":100023}],\"Subcantidades\":[{\"CantidadDistribuida\":20.5,\"Codigo\":\"CODIGO1234\",\"TipoCodigo\":\"TIPOCODI\"}]},{\"NroLinea\":34,\"Nombre\":\"Item de prueba2\",\"PrecioUnitario\":10,\"Descripcion\":\"Lorem ipsum dolor sit cuchuflí barquillo bacán jote gamba listeilor po cahuín, luca melón con vino pichanga coscacho ni ahí peinar la muñeca chuchada al chancho achoclonar. Chorrocientos pituto ubicatex huevo duro bolsero cachureo el hoyo del queque en cana huevón el año del loly hacerla corta impeque de miedo quilterry la raja longi ñecla. Hilo curado rayuela carrete quina guagua lorea piola ni ahí.\",\"Monto\":120,\"Cantidad\":12,\"Subdescuentos\":[{\"Tipo\":\"%\",\"Valor\":15},{\"Tipo\":\"$\",\"Valor\":100000}],\"CodigosItem\":[{\"Tipo\":\"INT6\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT7\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT8\",\"Codigo\":\"ASD2341\"},{\"Tipo\":\"INT9\",\"Codigo\":\"ASD2341\"}]}],\"DescuentosYRecargosGlobales\":[{\"NroLinea\":1,\"TipoMovimiento\":\"R\",\"Glosa\":\"Lorem ipsum dolor sit cuchuflí barquillo \",\"TipoValor\":\"$\",\"Valor\":40001.111,\"IndicadorExcecion\":1,\"ValorOtraMoneda\":200000.336}],\"ReferenciasGlobales\":[{\"NroLinea\":1,\"TipoDocumento\":\"DOC\",\"IndicadorGlobal\":1,\"Folio\":\"FE9000001\",\"RUTOtroContribuyente\":\"11111111-1\",\"Fecha\":\"2020-11-25\",\"Codigo\":1,\"Razon\":\"Razon que desconozco\"}],\"ComisionesYOtrosCargos\":[{\"NroLinea\":1,\"TipoMovimiento\":\"C\",\"Glosa\":\"Lorem ipsum dolor sit cuchuflí barquillo \",\"Tasa\":15.111,\"ValorNeto\":1000,\"ValorExento\":10000,\"ValorIVA\":40004}],\"Acciones\":[{\"Codigo\":\"ALGO\",\"NumeroImpresiones\":10,\"LogoDTE\":\"DD45\",\"Plantilla\":\"dirplantilla\"}],\"MailEnvio\":[{\"NroLinea\":1,\"Direccion\":\"uncorreito@cualquiera.com\",\"Copia\":\"uncorreito@cualquiera.com\",\"CopiaOculta\":\"uncorreito@cualquiera.com\",\"Mensaje\":\"Lorem ipsum dolor sit cuchuflí barquillo bacán jote gamba listeilor po cahuín, luca melón con vino pichanga coscacho ni ahí peinar la muñeca chuchada al chancho achoclonar. Chorrocientos pituto ubicatex huevo duro bolsero cachureo el hoyo del queque en cana huevón el año del loly hacerla corta impeque de miedo quilterry la raja longi ñecla. Hilo curado rayuela carrete quina guagua lorea piola ni ahí.\"}],\"Adjuntos\":[{\"NroLinea\":1,\"Tipo\":\"DOC\",\"Ruta\":\"C:\\\\ruta\\\\aqui\\\\cualquiera\"},{\"NroLinea\":2,\"Tipo\":\"TXT\",\"Ruta\":\"C:\\\\ruta\\\\aqui\\\\cualquieraXML\"}],\"ValoresLibres\":[\"Impresora\",\"Hola\",\"esto\",\"e\'s\",\"un\",\"Valor\",\"li\'bre\",\"Por\",\"Si\",\"Acaso\"]}', 2, '2022-04-25 00:45:03.968695', '2022-04-25 00:44:56.121332', NULL, NULL);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `parametro`
--

CREATE TABLE `parametro` (
  `Id` int(11) NOT NULL,
  `Llave` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `Tipo` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `Valor` longtext CHARACTER SET utf8mb4 DEFAULT NULL,
  `FechaModificacion` datetime(6) NOT NULL,
  `FechaCreacion` datetime(6) NOT NULL,
  `Activo` tinyint(1) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `parametro`
--

INSERT INTO `parametro` (`Id`, `Llave`, `Tipo`, `Valor`, `FechaModificacion`, `FechaCreacion`, `Activo`) VALUES
(1, 'IdentificadorEmpresa', 'string', '76257812', '2022-04-30 00:00:00.000000', '2022-04-30 00:00:00.000000', 1),
(2, 'UsuarioEmpresa', 'string', '76729840-4', '2022-04-30 00:00:00.000000', '2022-04-30 00:00:00.000000', 1),
(3, 'AutorizacionEmpresa', 'string', 'NDg5QzgzMDdEM0IzRDFBRDBFMUU', '2022-04-30 00:00:00.000000', '2022-04-30 00:00:00.000000', 1),
(4, 'URL_TOKEN', 'string', 'https://fewss02-cert.cl.dbnetcorp.com/API/Gw/GetToken', '2022-04-30 00:00:00.000000', '2022-04-30 00:00:00.000000', 1),
(5, 'TOKEN_TIME_MIN', 'int', '30', '2022-04-30 00:00:00.000000', '2022-04-30 00:00:00.000000', 1),
(6, 'URL_CARGA', 'string', 'https://fewss02-cert.cl.dbnetcorp.com/API/Gw/CargaDTE', '2022-04-30 00:00:00.000000', '2022-04-30 00:00:00.000000', 1),
(7, 'URL_DATA', 'string', 'https://demorexperian.free.beeceptor.com/api/documents', '2022-04-30 00:00:00.000000', '2022-04-30 00:00:00.000000', 1),
(8, 'URL_TOKEN_EXPERIAN', 'string', 'https://fewss02-cert.cl.dbnetcorp.com/API/Gw/GetToken', '2022-04-30 00:00:00.000000', '2022-04-30 00:00:00.000000', 1),
(9, 'EMAIL_SMTP', 'string', 'smtp.suiteelectronica.com ', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(10, 'EMAIL_TO', 'string', 'dte_cert_experian@smtp.suiteelectronica.com', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(11, 'EMAIL_PASS', 'string', 'Experian*2', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(12, 'EMAIL_PORT', 'int', '25', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(13, 'EMAIL_FROM', 'string', 'aneira.one@gmail.com', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(14, 'EMAIL_FROM', 'string', 'alex.blue.bird@hotmail.com', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(15, 'TOKEN_USER_EXPERIAN', 'string', 'DEMO', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(16, 'TOKEN_PASS_EXPERIAN', 'string', 'DEMO', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(17, 'CLIENTID_EXPERIAN', 'string', 'ASAS', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1),
(18, 'CLIENT_SECRET', 'string', 'ASDSAD', '2022-05-01 00:00:00.000000', '2022-05-01 00:00:00.000000', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(95) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Volcado de datos para la tabla `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20220425033737_init', '3.1.24'),
('20220502012916_InitialCreate', '3.1.24');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `documento`
--
ALTER TABLE `documento`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `parametro`
--
ALTER TABLE `parametro`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `documento`
--
ALTER TABLE `documento`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `parametro`
--
ALTER TABLE `parametro`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
