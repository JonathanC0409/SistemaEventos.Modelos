using SistemaEventos.ConsumeAPI;
using SistemaEventos.Modelos;

namespace SistemaEventos.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion;
            do
            {
                MostrarMenuPrincipal();
                opcion = ObtenerOpcion();

                switch (opcion)
                {
                    case 1:
                        GestionarEventos();
                        break;
                    case 2:
                        AsignarPonentes();
                        break;
                    case 3:
                        RegistrarParticipantes();
                        break;
                    case 4:
                        RealizarIncripcion();
                        break;
                    case 5:
                        RealizarPagos();
                        break;
                    case 6:
                        EmitirCertificado();
                        break;
                    case 7:
                        ConsultarInformacionHistorica();
                        break;
                    case 8:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }


            } while (opcion != 8);
        }


        public static void MostrarMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("Sistema de Gestión de Eventos Universitarios");
            Console.WriteLine("1. Gestionar Eventos\n" +
                              "2. Asiganar Ponentes invitados\n" +
                              "3. Registrar Participantes\n" +
                              "4. Realizar de Incripciones\n" +
                              "5. Registrar Pago\n" +
                              "6. Emitir Certificados\n" +
                              "7. Consultar Informacion historica  y estadistica sobre los eventos," +
                              "participantes, pagosy certificados emitidos\n" +
                              "8. Salir");
        }

        public static void MostrarMenuEvento()
        {
            Console.Clear();
            Console.WriteLine("Gestion de Eventos");
            Console.WriteLine("1. Crear Evento\n" +
                              "2. Modificar Evento\n" +
                              "3. Eliminar Evento\n" +
                              "4. Listar Eventos\n" +
                              "5. Gestionar Sesiones\n" +
                              "6. Volver");

        }

        public static void AsignarPonentes()
        {
            Console.WriteLine("Ponenetes con sus eventos");

            CRUD<Ponente>.EndPoint = "https://localhost:7090/api/Ponentes";
            var ponentes = CRUD<Ponente>.GetAll();
            Console.WriteLine("Lista de Ponentes");
            foreach (var p in ponentes)
            {
                Console.WriteLine($"ID: {p.Codigo}, Nombre: {p.Nombre}, Apellido: {p.Apellido}, Pais: {p.Pais}");
                foreach (var evento in p.Eventos)
                {
                    Console.WriteLine($"Codigo {evento.Codigo}, Evento: {evento.Nombre}, Fecha: {evento.Fecha.ToShortDateString()}, Lugar: {evento.Lugar}, Tipo: {evento.Tipo}");
                }
            }

            CrearEvento();
            Console.ReadLine();

        }

        public static void RegistrarParticipantes()
        {
            CRUD<Participante>.EndPoint = "https://localhost:7090/api/Participantes";
            var participantes = CRUD<Participante>.Create(new Participante
            {
                Nombre = ObtenerInput("Ingrese el nombre del participante:"),
                Facultad = ObtenerInput("Ingrese Facultad:"),
                Carrera = ObtenerInput("Ingrese Carrera:"),
                Nivel = ObtenerInput("Ingrese nivel"),
                AsistenciaCompleta = (ObtenerInput("Asistencia completa solo true o false:"))
            });

            if(participantes != null)
            {
                Console.WriteLine("Participante registrado exitosamente");
            }
            else
            {
                Console.WriteLine("Error al registrar participante");
            }
            Console.ReadLine();
        }

        public static void RealizarIncripcion()
        {
            CRUD<Inscripcion>.EndPoint = "https://localhost:7090/api/Inscripciones";
            var incripcion = CRUD<Inscripcion>.Create(new Inscripcion
            {
                Pago = (ObtenerInput("Esta Pagado true o false")),
                Estado = ObtenerInput("Ingrese estado Confirmado o Denegado"),
                FechaInscripcion = DateTime.Parse(ObtenerInput("Ingrese fecha dd/mm/yyyy")),
                ParticipanteCodigo = int.Parse(ObtenerInput("Ingrese codigo del participante"))
            });

            if(incripcion != null)
            {
                Console.WriteLine("Incripcion registrada exitosamente");
            }
            else
            {
                Console.WriteLine("Error al registrar incripcion");
            }
            Console.ReadLine();
        }

        public static void RealizarPagos()
        {
            CRUD<RegistroPago>.EndPoint = "https://localhost:7090/api/RegistroPagos";
            var registroPago = CRUD<RegistroPago>.Create(new RegistroPago
            {
                Nombre = ObtenerInput("Ingrese nombre del registro de pago:"),
                Monto = double.Parse(ObtenerInput("Ingrese monto")),
                TipoPago = ObtenerInput("Ingrese tipo de pago puede ser Efectivo o Tarjeta"),
                InscripcionCodigo = int.Parse(ObtenerInput("Ingrese codigo de la incripcion"))
            });
            if (registroPago != null)
            {
                Console.WriteLine("Pago registrado exitosamente");
            }
            else
            {
                Console.WriteLine("Error al registrar pago");
            }
            Console.ReadLine();
        }

        public static void EmitirCertificado()
        {
            CRUD<Certificado>.EndPoint = "https://localhost:7090/api/Certificados";
            CRUD<Participante>.EndPoint = "https://localhost:7090/api/Participantes";
            CRUD<Inscripcion>.EndPoint = "https://localhost:7090/api/Inscripciones";

            var participantes = CRUD<Participante>.GetAll();
            foreach (var p in participantes)
            {
                Console.WriteLine($"ID: {p.Codigo}, Nombre: {p.Nombre} Asistencia: {p.AsistenciaCompleta}");
                if (p.AsistenciaCompleta.Equals("true"))
                {
                    foreach (var i in p.Inscripciones)
                    {
                        if (i.Pago.Equals("true"))
                        {
                            Console.WriteLine($"ID: {i.Codigo}, Estado: {i.Estado}, Fecha: {i.FechaInscripcion.ToShortDateString()}");
                            CRUD<Certificado>.Create(new Certificado
                            {
                                Nombre = ObtenerInput("Ingrese nombre del certificado:"),
                                TipoCertificado = "Aprobado",
                                ParticipanteCodigo = p.Codigo
                            });
                            Console.WriteLine("CERTIFICADO GENERADO");
                        }
                        else
                        {
                            Console.WriteLine("El participante no ha pagado la inscripción, no se puede emitir el certificado.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine("El participante no asistió completamente, no se puede emitir el certificado.");
                }
            }
        }

        public static void ConsultarInformacionHistorica()
        {
            Console.Clear();
            Console.WriteLine("Información Histórica y Estadística\n");

            // Establecer endpoints
            CRUD<Evento>.EndPoint = "https://localhost:7090/api/Eventos";
            CRUD<Participante>.EndPoint = "https://localhost:7090/api/Participantes";
            CRUD<Inscripcion>.EndPoint = "https://localhost:7090/api/Inscripciones";
            CRUD<RegistroPago>.EndPoint = "https://localhost:7090/api/RegistroPagos";
            CRUD<Certificado>.EndPoint = "https://localhost:7090/api/Certificados";

            // Obtener datos
            var eventos = CRUD<Evento>.GetAll();
            var participantes = CRUD<Participante>.GetAll();
            var inscripciones = CRUD<Inscripcion>.GetAll();
            var pagos = CRUD<RegistroPago>.GetAll();
            var certificados = CRUD<Certificado>.GetAll();

            // Estadísticas generales
            Console.WriteLine($"?? Total de Eventos: {eventos?.Count() ?? 0}");
            Console.WriteLine($"?? Total de Participantes: {participantes?.Count() ?? 0}");
            Console.WriteLine($"?? Total de Certificados Emitidos: {certificados?.Count() ?? 0}");
            Console.WriteLine($"?? Total de Pagos Registrados: {pagos?.Count() ?? 0}");

            double montoTotal = pagos?.Sum(p => p.Monto) ?? 0;
            Console.WriteLine($"?? Suma Total de Monto Recaudado: ${montoTotal:0.00}\n");

            // Participantes con certificados
            Console.WriteLine("--- Participantes con certificados emitidos ---");
            if (certificados != null && participantes != null)
            {
                foreach (var cert in certificados)
                {
                    var participante = participantes.FirstOrDefault(p => p.Codigo == cert.ParticipanteCodigo);
                    if (participante != null)
                    {
                        Console.WriteLine($"Participante: {participante.Nombre}, Certificado: {cert.Nombre}, Tipo: {cert.TipoCertificado}");
                    }
                }
            }

            Console.WriteLine();

            // Pagos registrados por inscripción
            Console.WriteLine("--- Pagos registrados por inscripción ---");
            if (pagos != null)
            {
                foreach (var pago in pagos)
                {
                    Console.WriteLine($"Pago: {pago.Codigo}, Tipo: {pago.TipoPago}, Monto: {pago.Monto}, Inscripción Código: {pago.InscripcionCodigo}");
                }
            }

            Console.WriteLine("\nPresione Enter para volver al menú principal...");
            Console.ReadLine();
        }



        public static void GestionarEventos()
        {
            Console.Clear();
            Console.WriteLine("Gestionar Eventos");
            int opcionEvento;
            do
            {
                MostrarMenuEvento();
                opcionEvento = ObtenerOpcion();
                switch (opcionEvento)
                {
                    case 1:
                        CrearEvento();
                        Console.ReadLine();
                        break;
                    case 2:
                        ModificarEvento();
                        Console.ReadLine();

                        break;
                    case 3:
                        EliminarEvento();
                        Console.ReadLine();

                        break;
                    case 4:
                        ListarEventos();
                        Console.ReadLine();

                        break;
                    case 5:
                        GestionarSesiones();
                        Console.ReadLine();
                        break;
                    case 6:
                        Console.WriteLine("Volviendo al menú principal...");
                        Console.ReadLine();
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            } while (opcionEvento != 6);
        }

        public static void CrearEvento()
        {
            var nombre = ObtenerInput("Ingrese el nombre del evento:");
            var fecha = ObtenerInput("Ingrese la fecha del evento (dd/MM/yyyy):");
            var lugar = ObtenerInput("Ingrese el lugar del evento:");
            var tipo = ObtenerInput("Ingrese el tipo de evento:");
            var ponente = ObtenerInput("Ingrese el codigo del ponente:");
            var participante = ObtenerInput("Ingrese el codigo del participante:");

            CRUD<Evento>.EndPoint = "https://localhost:7090/api/Eventos";
            var evento = CRUD<Evento>.Create(new Evento
            {
                Nombre = nombre,
                Fecha = DateTime.Parse(fecha),
                Lugar = lugar,
                Tipo = tipo,
                PonenteCodigo = int.Parse(ponente),
                ParticipanteCodigo = int.Parse(participante)

            });

            Console.WriteLine(evento != null ? "Evento Creado" : "Error al crear Evento");

        }

        public static void ModificarEvento()
        {
            CRUD<Evento>.EndPoint = "https://localhost:7090/api/Eventos";
            var eventoID = int.Parse(ObtenerInput("Ingrese el ID del evento a modificar:"));
            var evento = CRUD<Evento>.GetById(eventoID);

            if (evento != null)
            {
                Console.WriteLine($"Evento encontrado: {evento.Nombre}");
                var nombre = ObtenerInput("Ingrese el nuevo nombre del evento (deje vacío para no modificar):");
                var fecha = ObtenerInput("Ingrese la nueva fecha del evento (dd/MM/yyyy, deje vacío para no modificar):");
                var lugar = ObtenerInput("Ingrese el nuevo lugar del evento (deje vacío para no modificar):");
                var tipo = ObtenerInput("Ingrese el nuevo tipo de evento (deje vacío para no modificar):");

                // Modificar los valores solo si el usuario ingresa un nuevo valor
                evento.Nombre = string.IsNullOrEmpty(nombre) ? evento.Nombre : nombre;
                evento.Fecha = string.IsNullOrEmpty(fecha) ? evento.Fecha : DateTime.Parse(fecha);
                evento.Lugar = string.IsNullOrEmpty(lugar) ? evento.Lugar : lugar;
                evento.Tipo = string.IsNullOrEmpty(tipo) ? evento.Tipo : tipo;

                var resultado = CRUD<Evento>.Update(evento.Codigo, evento);
                Console.WriteLine(resultado ? "Evento modificado exitosamente" : "Error al modificar evento");
            }
            else
            {
                Console.WriteLine("Evento no encontrado.");
            }
        }

        public static void EliminarEvento()
        {
            CRUD<Evento>.EndPoint = "https://localhost:7090/api/Eventos";
            var eventoID = int.Parse(ObtenerInput("Ingrese el ID del evento a eliminar:"));
            var evento = CRUD<Evento>.GetById(eventoID);

            if (evento != null)
            {
                var confirmacion = ObtenerInput("¿Está seguro de eliminar este evento? (s/n):");
                if (confirmacion.ToLower() == "s")
                {
                    var resultado = CRUD<Evento>.Delete(evento.Codigo);
                    Console.WriteLine(resultado ? "Evento eliminado exitosamente" : "Error al eliminar evento");
                }
                else
                {
                    Console.WriteLine("Eliminación cancelada.");
                }
            }
            else
            {
                Console.WriteLine("Evento no encontrado.");
            }
        }

        public static void ListarEventos()
        {
            CRUD<Evento>.EndPoint = "https://localhost:7090/api/Eventos";
            var eventos = CRUD<Evento>.GetAll();

            if (eventos != null && eventos.Any())
            {
                Console.WriteLine("Lista de Eventos:");
                foreach (var evento in eventos)
                {
                    Console.WriteLine($"ID: {evento.Codigo}, Nombre: {evento.Nombre}, Fecha: {evento.Fecha.ToShortDateString()}, Lugar: {evento.Lugar}, Tipo: {evento.Tipo}");
                }
            }
            else
            {
                Console.WriteLine("No se han encontrado eventos.");
            }
        }

        public static void GestionarSesiones()
        {
            Console.Clear();
            Console.WriteLine("Gestionar Sesiones");
            int opcionSesion;
            do
            {
                MostrarMenuSesion();
                opcionSesion = ObtenerOpcion();
                switch (opcionSesion)
                {
                    case 1:
                        CrearSesion();
                        Console.ReadLine();
                        break;
                    case 2:
                        ModificarSesion();
                        Console.ReadLine();
                        break;
                    case 3:
                        EliminarSesion();
                        Console.ReadLine();
                        break;
                    case 4:
                        ListarSesiones();
                        Console.ReadLine();
                        break;
                    case 5:
                        Console.WriteLine("Volviendo al menú de eventos...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            } while (opcionSesion != 5);
        }

        public static void MostrarMenuSesion()
        {
            Console.Clear();
            Console.WriteLine("Gestionar Sesiones");
            Console.WriteLine("1. Crear Sesión\n" +
                              "2. Modificar Sesión\n" +
                              "3. Eliminar Sesión\n" +
                              "4. Listar Sesiones\n" +
                              "5. Volver al menú de eventos");
        }

        public static void CrearSesion()
        {
            var nombre = ObtenerInput("Ingrese el nombre de la Sesion:");
            var inicio = ObtenerInput("Ingrese la hora de inicio");
            var fin = ObtenerInput("Ingrese la hora de fin:");
            var sala = ObtenerInput("Ingrese nombre sala:");
            var evento = ObtenerInput("Ingrese el codigo del evento:");

            CRUD<Sesion>.EndPoint = "https://localhost:7090/api/Sesiones";
            var sesion = CRUD<Sesion>.Create(new Sesion
            {
                Nombre = nombre,
                HorarioInicio = int.Parse(inicio),
                HorarioFin = int.Parse(fin),
                Sala = sala,
                EventoCodigo = int.Parse(evento)
            });

            Console.WriteLine(evento != null ? "Sesion Creada" : "Error al crear Sesion");

        }

        public static void ModificarSesion()
        {
            CRUD<Sesion>.EndPoint = "https://localhost:7090/api/Sesiones";
            var sesionId = int.Parse(ObtenerInput("Ingrese el ID de la sesion a modificar:"));
            var sesion = CRUD<Sesion>.GetById(sesionId);

            if (sesion != null)
            {
                Console.WriteLine($"Evento encontrado: {sesion.Nombre}");
                var nombre = ObtenerInput("Ingrese el nuevo nombre de la sesion (deje vacío para no modificar):");
                var inicio = ObtenerInput("Ingrese inicio (deje vacío para no modificar)");
                var fin = ObtenerInput("Ingrese fin: (deje vacío para no modificar)");
                var sala = ObtenerInput("Ingrese nombre sala (deje vacío para no modificar):");
                var eventoId = ObtenerInput("Ingrese el codigo del evento):");


                // Modificar los valores solo si el usuario ingresa un nuevo valor
                sesion.Nombre = string.IsNullOrEmpty(nombre) ? sesion.Nombre : nombre;
                sesion.HorarioInicio = string.IsNullOrEmpty(inicio) ? sesion.HorarioInicio : int.Parse(inicio);
                sesion.HorarioFin = string.IsNullOrEmpty(fin) ? sesion.HorarioFin : int.Parse(fin);
                sesion.Sala = string.IsNullOrEmpty(sala) ? sesion.Sala : sala;
                sesion.EventoCodigo = string.IsNullOrEmpty(eventoId) ? sesion.EventoCodigo : int.Parse(eventoId);

                var resultado = CRUD<Sesion>.Update(sesion.Codigo, sesion);
                Console.WriteLine(resultado ? "Sesion modificado exitosamente" : "Error al modificar sesion");
            }
            else
            {
                Console.WriteLine("sesion no encontrado.");
            }
        }

        public static void EliminarSesion()
        {
            CRUD<Sesion>.EndPoint = "https://localhost:7090/api/Sesiones";
            var sesionId = int.Parse(ObtenerInput("Ingrese el ID de de la sesion a eliminar:"));
            var sesion = CRUD<Sesion>.GetById(sesionId);

            if (sesion != null)
            {
                var confirmacion = ObtenerInput("¿Está seguro de eliminar est sesion? (s/n):");
                if (confirmacion.ToLower() == "s")
                {
                    var resultado = CRUD<Sesion>.Delete(sesion.Codigo);
                    Console.WriteLine(resultado ? "Sesion eliminado exitosamente" : "Error al eliminar sesion");
                }
                else
                {
                    Console.WriteLine("Eliminación cancelada.");
                }
            }
            else
            {
                Console.WriteLine("Sesion no encontrado.");
            }
        }

        public static void ListarSesiones()
        {
            CRUD<Sesion>.EndPoint = "https://localhost:7090/api/Sesiones";
            var sesiones = CRUD<Sesion>.GetAll();

            if (sesiones != null && sesiones.Any())
            {
                Console.WriteLine("Lista de Sesiones:");
                foreach (var s in sesiones)
                {
                    Console.WriteLine($"ID: {s.Codigo}, Nombre: {s.Nombre}, Inicio: {s.HorarioInicio}, Fin: {s.HorarioFin}, Sala: {s.Sala},");
                }
            }
            else
            {
                Console.WriteLine("No se han encontrado eventos.");
            }
        }


        public static int ObtenerOpcion()
        {
            int opcion;
            while (true)
            {
                Console.Write("Seleccione una opción: ");
                if (int.TryParse(Console.ReadLine(), out opcion))
                    break;
                else
                    Console.WriteLine("Opción no válida, intente nuevamente.");
            }
            return opcion;
        }

        static string ObtenerInput(string mensaje)
        {
            Console.WriteLine(mensaje);
            return Console.ReadLine();
        }
    }
}
