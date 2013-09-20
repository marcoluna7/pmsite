using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LogicLayer;

namespace LogicLayer.DbLayer
{
    public class AccesoSql : AcessDb, IDisposable
    {
        private pmsiteEntities _dc;
        private bool _disposed;
        public AccesoSql()
        {
            _dc = new pmsiteEntities();            
        }

        public override users ValidateUser(string userName, string password)
        {
            var consulta = _dc.users.Where(u=>u.userName.ToLower() == userName.ToLower() && u.password == password);
            if (consulta.Count() > 0)
            {
                return consulta.First();
            }
            return null;
        }

        public override bool InsertUser(users objUser)
        {
            bool bandera = false;
            var consulta = _dc.users.Where(u=> u.userName == objUser.userName && objUser.id == 0);
            if (consulta.Count() == 0)
            {
                _dc.users.Add(objUser);
                _dc.SaveChanges();
                bandera = true;
            }
            return bandera;
        }

        public override users getUserByUserName(string userName)
        {
            var consulta = _dc.users.Where(u=> u.userName == userName);
            if (consulta.Count() > 0)
            {
                return consulta.First();
            }
            return null;
        }

        public override void ConfirmarCambios()
        {
            _dc.SaveChanges();
        }

        public override void insertLogEvent(logEventos objLog)
        {
            _dc.logEventos.Add(objLog);
            _dc.SaveChanges();
        }

        public override void insertEvents(pm_events evento)
        {
            if (evento.uid == 0)
            {
                _dc.pm_events.Add(evento);
                _dc.SaveChanges();
            }
            else
            {
                _dc.SaveChanges();
            }
        }

        public override List<pm_events> getEvents(int? id, string name, DateTime? dateI, DateTime? dateF)
        {
            var consulta = from e in _dc.pm_events
                           where (id == null || e.uid == id) &&
                           (string.IsNullOrEmpty(name) || e.name.ToLower().Contains(name.ToLower())) &&
                           (dateI == null || e.eventDate >= dateI && e.eventDate <= dateF)
                           select e;
            if(consulta.Count() >0)
                return consulta.ToList();
            return null;
        }

        public override List<pm_speaker> getSpeakers(int? id, string name)
        {
            var consulta = from s in _dc.pm_speaker
                           where (id == null || s.uid == id) &&
                           (string.IsNullOrEmpty(name) || s.firstName.ToLower().Contains(name.ToLower()) || s.lastName.ToLower().Contains(name.ToLower()) ) 
                           select s;
            if(consulta.Count() > 0)
            {
                return consulta.ToList();
            }
            return null;
        }

        public override List<pm_user> getPmUsers(int? id, string name)
        {
            var consulta = from u in _dc.pm_user
                           where (id == null || u.uid == id) &&
                           (string.IsNullOrEmpty(name) || u.otherName.ToLower().Contains(name.ToLower()) || u.firstName.ToLower().Contains(name.ToLower()) || u.lastName.ToLower().Contains(name.ToLower()))
                           select u;
            if (consulta.Count() > 0)
            {
                return consulta.ToList();
            }
            return null;
        }

        public override bool insertPmSpeaker(pm_speaker objSpeak)
        {
            bool bandera = false;
            if (objSpeak.uid > 0)
            {
                _dc.pm_speaker.Add(objSpeak);
                _dc.SaveChanges();
                bandera = true;
            }
            return bandera;
        }

        public override bool insertPmUser(pm_user objUser)
        {
            bool bandera = false;
            var consulta = _dc.pm_user.Where(u=>u.registrationId == objUser.registrationId);
            if (consulta.Count() > 0)
            {
                var objUpdate = consulta.First();
                objUser.uid = objUpdate.uid;
            }
            else 
            {
                _dc.pm_user.Add(objUser);
                _dc.SaveChanges();
                bandera = true;
            }
            return bandera;
        }

        public override bool insertSpeakEvent(int idEvent, int idSpeak)
        {
            bool bandera = false;
            var consulta = _dc.pm_speakers_event.Where(se=>se.event_id == idEvent && se.speaker_id == idSpeak);
            if (consulta.Count() == 0)
            {
                _dc.pm_speakers_event.Add(new pm_speakers_event() { speaker_id=idSpeak , event_id=idEvent , createdDate = DateTime.Now });
                _dc.SaveChanges();
                bandera = true;
            }
            return bandera;
        }

        public override bool deleteSpeakEvent(int idEvent, int idSpeak)
        {
            bool bandera = false;
            var consulta = _dc.pm_speakers_event.Where(se => se.event_id == idEvent && se.speaker_id == idSpeak);

            if (consulta.Count() > 0)
            {
                _dc.pm_speakers_event.Remove(consulta.First());
                _dc.SaveChanges();
                bandera = true;
            }
            return bandera;

        }

        public override bool insertUserEvent(pm_rsvp objUE)
        {
            bool bandera = false;
            if (objUE.event_id > 0 && objUE.user_id > 0)
            {
                var consulta = _dc.pm_rsvp.Where(ue=> ue.user_id == objUE.user_id && ue.event_id == objUE.event_id);
                if (consulta.Count() == 0)
                {
                    _dc.pm_rsvp.Add(objUE);
                    _dc.SaveChanges();
                    bandera = true;
                }
                else
                {
                    var objUpdate = consulta.First();
                    objUpdate.active = objUE.active;
                    objUpdate.modifiedDate = DateTime.Now;
                    _dc.SaveChanges();
                    bandera = true;
                }
            }
         
            return bandera;
        }

        public override List<pm_user> GetPmUsersByEvent(int idEvent)
        {
            var consulta = from eu in _dc.pm_rsvp
                           join u in _dc.pm_user on eu.user_id equals u.uid
                           where eu.event_id == idEvent
                           select u;

            if (consulta.Count() > 0)
            {
                return consulta.ToList();
            }
            return null;            
        }

        public override List<pm_speaker> GetPmSpeakersByEvent( int idEvent)
        {
            var consulta = from es in _dc.pm_speakers_event
                           join s in _dc.pm_speaker on es.speaker_id equals s.uid
                           where es.event_id == idEvent
                           select s;

            if (consulta.Count() > 0)
            {
                return consulta.ToList();
            }
            return null;
        }

        public override List<pm_events> GetEventsByUser(int idUsuario)
        {
            var consulta = from ue in _dc.pm_rsvp
                           join e in _dc.pm_events on ue.event_id equals e.uid
                           where ue.user_id == idUsuario
                           select e;
            if (consulta.Count() > 0)
            {
                return consulta.ToList();
            }
            return null;
        }

        public override List<pm_events> GetEventsBySpeak(int idSpeak)
        {
            var consulta = from se in _dc.pm_speakers_event
                           join e in _dc.pm_events on se.event_id equals e.uid
                           where se.speaker_id == idSpeak
                           select e;

            if (consulta.Count() > 0)
            {
                return consulta.ToList();
            }
            return null;
        }

        
        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_dc != null)
                    {
                        
                        _dc.Dispose();
                    }
                }
                _dc = null;
                _disposed = true;
            }
        }
    }
}
