using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicLayer;

namespace LogicLayer.DbLayer
{
    public abstract class AcessDb : IDisposable
    {
        abstract public users ValidateUser(string userName, string password);
        abstract public bool InsertUser(users objUser);
        abstract public void Dispose();
        abstract public users getUserByUserName(string userName);
        abstract public void ConfirmarCambios();
        abstract public void insertLogEvent(logEventos objLog);
        abstract public void insertEvents(pm_events evento);
        abstract public List<pm_events> getEvents(int? id, string name, DateTime? dateI, DateTime? dateF);
        abstract public List<pm_speaker> getSpeakers(int? id, string name);
        abstract public List<pm_user> getPmUsers(int? id, string name);
        abstract public bool insertPmSpeaker(pm_speaker objSpeak);
        abstract public bool insertPmUser(pm_user objUser);
        abstract public bool insertSpeakEvent(int idEvent, int idSpeak);
        abstract public bool deleteSpeakEvent(int idEvent, int idSpeak);
        abstract public bool insertUserEvent(pm_rsvp objUE);
        abstract public List<pm_user> GetPmUsersByEvent(int idEvent);
        abstract public List<pm_speaker> GetPmSpeakersByEvent(int idEvent);
        abstract public List<pm_events> GetEventsByUser(int idUsuario);
        abstract public List<pm_events> GetEventsBySpeak(int idSpeak);
        
        abstract public void Dispose(bool disposing);

        public static AcessDb GetInstance()
        {
            return new AccesoSql();
        }
    }
}
