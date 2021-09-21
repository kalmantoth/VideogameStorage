import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http'
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { IVideogame } from './videogame.model';
import { VIDEOGAMES } from './mock-videogames';

@Injectable({
  providedIn: 'root'
})

export class VideogameService {

  constructor(private http: HttpClient) { }


  getVideogames(offset : number, limit: number):Observable<IVideogame[]> {
    let apiURL = `http://localhost:5000/api/v1/Videogames/${offset}/${limit}`;
    return this.http.get<IVideogame[]>(apiURL)
      .pipe(
        tap(_ => console.log("videogames fetched...")),
        catchError(this.handleError<IVideogame[]>('getVideogames', []))
        )
  }

  updateVideogame(videogame: IVideogame) : Observable<IVideogame> {
    let options = { headers: new HttpHeaders({'Content-Type': 'application/json-patch+json'})}; 
    let apiURL = "http://localhost:5000/api/v1/Videogames/" + (videogame.videogameId);
    return this.http.put<IVideogame>(apiURL , videogame)
      .pipe(
        tap(_ => console.log("videogame updated...")),
        catchError(this.handleError<IVideogame>('updateVideogame', videogame))
        );
  }

  private handleError<T> (operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      console.error(error);
      return of(result as T);
    }
  }

}
