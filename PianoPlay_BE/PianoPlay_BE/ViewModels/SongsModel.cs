using PianoPlay_BE.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PianoPlay_BE.Model
{
    public class SongsModel
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public string KeyIds { get; set; }
        public string CreatedDate { get; set; }

        public SongsModel() { }
        public SongsModel(Songs song)
        {
            Id = song.Id;
            UserId = song.UserId;
            Name = song.Name;
            KeyIds = song.KeyIds;
            CreatedDate = song.CreatedDate;
        }
    }
    public class CreateSongModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string KeyIds { get; set; }
        public string CreatedDate { get; set; }
    }
    public class EditSongModel : CreateSongModel
    {
        public long id { get; set; }
        //public string songCode { get; set; }
    }
}