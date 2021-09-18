import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

import { IVideogame } from './videogame.model';
import { VIDEOGAMES } from './mock-videogames';

@Injectable({
  providedIn: 'root'
})

export class VideogameService {

  constructor() { }

  getVideogames(): Observable<IVideogame[]>{
    const videogames = of(VIDEOGAMES);
    return videogames;
  }
}
