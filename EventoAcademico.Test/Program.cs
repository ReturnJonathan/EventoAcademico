using System;
using System.Collections.Generic;
using EventoAcademico.Modelos;
using EventoAcademico.API.Consumer;
using EventoAcademico.Api.Consumer;

namespace EventoAcademico.Test
{
    internal class Program
    {
        private static void Main(string[] args)
        {
             ProbarEventos();
             ProbarSesiones();
             ProbarPonentes();
             ProbarParticipantes();
             ProbarInscripciones();
              ProbarPagos();
              ProbarCertificados();
            //ProbarControlCertificado();
            Console.WriteLine("Pruebas completadas. Pulsa ENTER para salir.");
            Console.ReadLine();
        }

        private static void ProbarEventos()
        {
            Console.WriteLine("=== EVENTOS ===");
            Crud<Evento>.EndPoint = "https://localhost:7184/api/Eventos";

            // Crear (Código fijo = 1)
            var ev = Crud<Evento>.Create(new Evento
            {
                Codigo = 8,
                Nombre = "Conferencia .NET 8",
                Fecha = DateOnly.FromDateTime(DateTime.Today.AddDays(7)),
                Lugar = "Auditorio Central",
                Tipo = "Conferencia"
            });
            Console.WriteLine($"Creado Evento: ID={ev.Codigo}, Nombre={ev.Nombre}, Lugar={ev.Lugar}");

            // Actualizar
            ev.Lugar = "Sala Principal";
            Crud<Evento>.Update(ev.Codigo, ev);

            // Obtener y mostrar actualizado
            var evUpd = Crud<Evento>.GetById(ev.Codigo);
            Console.WriteLine($"Actualizado Evento: ID={evUpd.Codigo}, Lugar={evUpd.Lugar}");
            Console.WriteLine();
        }

        private static void ProbarSesiones()
        {
            Console.WriteLine("=== SESIONES ===");
            Crud<Sesion>.EndPoint = "https://localhost:7184/api/Sesiones";

            // Crear (Código fijo = 1)
            var ses = Crud<Sesion>.Create(new Sesion
            {
                Codigo = 8,
                HorarioInicio = TimeOnly.Parse("09:00:00"),
                HorarioFin = TimeOnly.Parse("11:00:00"),
                Sala = "Sala A",
                CodigoEvento = 8
            });
            Console.WriteLine($"Creada Sesión: ID={ses.Codigo}, Sala={ses.Sala}");

            // Actualizar
            ses.Sala = "Sala B";
            Crud<Sesion>.Update(ses.Codigo, ses);

            // Obtener y mostrar actualizado
            var sesUpd = Crud<Sesion>.GetById(ses.Codigo);
            Console.WriteLine($"Actualizada Sesión: ID={sesUpd.Codigo}, Sala={sesUpd.Sala}");
            Console.WriteLine();
        }

        private static void ProbarParticipantes()
        {
            Console.WriteLine("=== PARTICIPANTES ===");
            Crud<Participante>.EndPoint = "https://localhost:7184/api/Participantes";

            // Crear (Código fijo = 1)
            var p = Crud<Participante>.Create(new Participante
            {
                Codigo = 8,
                Nombre = "Ana",
                Apellido = "Pérez",
                Institucion = "UTN",
                Correo = "ana.perez@utn.edu"
            });
            Console.WriteLine($"Creado Participante: ID={p.Codigo}, Nombre={p.Nombre} {p.Apellido}");

            // Actualizar
            p.Institucion = "UTN Ibarra";
            Crud<Participante>.Update(p.Codigo, p);

            // Obtener y mostrar actualizado
            var pUpd = Crud<Participante>.GetById(p.Codigo);
            Console.WriteLine($"Actualizado Participante: ID={pUpd.Codigo}, Institución={pUpd.Institucion}");
            Console.WriteLine();
        }
        private static void ProbarPonentes()
        {
            Console.WriteLine("=== PONENTES ===");
            Crud<Ponente>.EndPoint = "https://localhost:7184/api/Ponentes";

            // Crear (Código fijo = 6)
            var pon = Crud<Ponente>.Create(new Ponente
            {
                Codigo = 8,
                Nombre = "Juan",
                Apellido = "Gómez",
                Institucion = "Universidad X",
                Correo = "juan.gomez@universidadx.edu",
                CodigoEvento = 8,  // Debe existir previamente un Evento con Código = 6
                CodigoSesion = 8   // Debe existir previamente una Sesión con Código = 6
            });
            Console.WriteLine($"Creado Ponente: ID={pon.Codigo}, Nombre={pon.Nombre} {pon.Apellido}");

            // Actualizar
            pon.Institucion = "Universidad Y";
            Crud<Ponente>.Update(pon.Codigo, pon);

            // Obtener y mostrar actualizado
            var ponUpd = Crud<Ponente>.GetById(pon.Codigo);
            Console.WriteLine($"Actualizado Ponente: ID={ponUpd.Codigo}, Institución={ponUpd.Institucion}");
            Console.WriteLine();
        }

        private static void ProbarControlCertificado()
        {
            Console.WriteLine("=== CONTROL DE EMISIÓN DE CERTIFICADOS ===");
            var baseUrl = "https://localhost:7184/api/Certificados";
            var inscripcionId = 7; // asegúrate de que esta inscripción ya exista

            // 1) Intento sin haber hecho el pago
            try
            {
                var dto1 = new Certificado
                {
                    CodigoInscripcion = inscripcionId,
                    Asistencia = true,  // aunque marques asistencia…
                    TipoCertificado = "Test",
                    NombreCertificado = "CertSinPago",
                    Descripcion = "Debe fallar por falta de pago"
                };
                var cert1 = CertificadoClient.Emitir(baseUrl, inscripcionId, dto1);
                Console.WriteLine($"ERROR: Se creó certificado sin pago: ID={cert1.Codigo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Correcto (sin pago): {ex.Message}");
            }

            // 2) Registramos el pago
            Crud<Pago>.EndPoint = "https://localhost:7184/api/Pagos";
            var pago = Crud<Pago>.Create(new Pago
            {
                CodigoInscripcion = inscripcionId,
                MetodoPago = "Tarjeta",
                Monto = 200,
                Pagado = true
            });
            Console.WriteLine($"Pago registrado: ID={pago.Codigo}, Pagado={pago.Pagado}");

            // 3) Intento con pago pero sin asistencia
            try
            {
                var dto2 = new Certificado
                {
                    CodigoInscripcion = inscripcionId,
                    Asistencia = false, // no marcaste asistencia
                    TipoCertificado = "Test",
                    NombreCertificado = "CertSinAsistencia",
                    Descripcion = "Debe fallar por falta de asistencia"
                };
                var cert2 = CertificadoClient.Emitir(baseUrl, inscripcionId, dto2);
                Console.WriteLine($"ERROR: Se creó certificado sin asistencia: ID={cert2.Codigo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Correcto (sin asistencia): {ex.Message}");
            }

            // 4) Intento final con pago y asistencia
            try
            {
                var dto3 = new Certificado
                {
                    CodigoInscripcion = inscripcionId,
                    Asistencia = true,  // ahora sí
                    TipoCertificado = "Asistente Completo",
                    NombreCertificado = "CertFinal",
                    Descripcion = "Debe emitirse correctamente"
                };
                var cert3 = CertificadoClient.Emitir(baseUrl, inscripcionId, dto3);
                Console.WriteLine($"Certificado emitido OK: ID={cert3.Codigo}, Fecha={cert3.FechaEmision}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR inesperado: {ex.Message}");
            }

            Console.WriteLine();
        }

        private static void ProbarInscripciones()
        {
            Console.WriteLine("=== INSCRIPCIONES ===");
            Crud<Inscripcion>.EndPoint = "https://localhost:7184/api/Inscripciones";

            // Crear (Código fijo = 1)
            var i = Crud<Inscripcion>.Create(new Inscripcion
            {
                Codigo = 8,
                Estado = true,
                FechaInscripcion = DateOnly.FromDateTime(DateTime.Today),
                CodigoEvento = 8,
                CodigoParticipante = 8,
                CodigoSesion = 8
            });
            Console.WriteLine($"Creada Inscripción: ID={i.Codigo}, Estado={i.Estado}");

            // Actualizar
            i.Estado = false;
            Crud<Inscripcion>.Update(i.Codigo, i);

            // Obtener y mostrar actualizado
            var iUpd = Crud<Inscripcion>.GetById(i.Codigo);
            Console.WriteLine($"Actualizada Inscripción: ID={iUpd.Codigo}, Estado={iUpd.Estado}");
            Console.WriteLine();
        }

        private static void ProbarPagos()
        {
            Console.WriteLine("=== PAGOS ===");
            Crud<Pago>.EndPoint = "https://localhost:7184/api/Pagos";

            // Crear (Código fijo = 1)
            var p = Crud<Pago>.Create(new Pago
            {
                CodigoInscripcion = 8,
                Codigo = 8,
                MetodoPago = "Transferencia",
                Monto = 100,
                Pagado = true
            });
            Console.WriteLine($"Creado Pago: ID={p.Codigo}, Monto={p.Monto}, Pagado={p.Pagado}");

            // Actualizar
            p.Monto = 120;
            Crud<Pago>.Update(p.Codigo, p);

            // Obtener y mostrar actualizado
            var pUpd = Crud<Pago>.GetById(p.Codigo);
            Console.WriteLine($"Actualizado Pago: ID={pUpd.Codigo}, Monto={pUpd.Monto}");
            Console.WriteLine();
        }

        private static void ProbarCertificados()
        {
            Console.WriteLine("=== CERTIFICADOS ===");
            Crud<Certificado>.EndPoint = "https://localhost:7184/api/Certificados";

            // Crear certificado (Código fijo = 1)
            var certificado = Crud<Certificado>.Create(new Certificado
            {
                CodigoInscripcion = 8,
                Codigo = 8,
                Asistencia = true,
                TipoCertificado = "Asistente Completo",
                NombreCertificado = "CertificadoOK",
                Descripcion = "Cumplió requisitos"
            });
            Console.WriteLine($"Creado Certificado: ID={certificado.Codigo}, Asistencia={certificado.Asistencia}");

            // Actualizar descripción
            certificado.Descripcion = "Actualizado OK";
            Crud<Certificado>.Update(certificado.Codigo, certificado);

            // Obtener y mostrar actualizado
            var certUpd = Crud<Certificado>.GetById(certificado.Codigo);
            Console.WriteLine($"Actualizado Certificado: ID={certUpd.Codigo}, Descripción={certUpd.Descripcion}");
            Console.WriteLine();
        }
    }
}