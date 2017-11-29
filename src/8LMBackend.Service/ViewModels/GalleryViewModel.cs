using _8LMBackend.DataAccess.Models;
using System.Collections.Generic;

public class GalleryViewModel {
    public List<Gallery> Logos {get;set;} 
    public List<Gallery> Images {get;set;}
    public int TotalPages {get;set;}
}