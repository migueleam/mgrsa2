using mgrsa2.Common;
using mgrsa2.ViewModels;
using System.Collections.Generic;
namespace mgrsa2.Services
{
    public interface IEventServices
    {
        List<Evento> ListEventos(int id, string nivelarea, string oficinas
            , string depts   
            , string usuarios 
            , string nivelapp
           );

        //ResponseModel Guardar(EventVM evento);
        ResponseModel Insert(Evento evento);
        ResponseModel Update(Evento evento);
        ResponseModel UpdateNewDate(Evento evento);
        Evento GetEvento(int eventoId);
        ResponseModel Delete(Evento evento);
        ResponseModel SendNotificacion(Evento evento, string email, string subject); 

    }
}
