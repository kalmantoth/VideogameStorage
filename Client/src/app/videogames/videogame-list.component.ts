import { Component, OnInit } from '@angular/core';
import { IVideogame } from '../shared/videogame.model';

@Component({
  templateUrl: './videogame-list.component.html',
  styles: [`
  .container-sm{
    margin-top: 25px; 
    max-width:1000px;
  }

  .container-sm span{
      color:rgb(255, 255, 255);
  }
  `]
})
export class VideogameListComponent implements OnInit {

  videogames!: IVideogame[];

  constructor() { }

  ngOnInit(): void {
    this.videogames = [
      {
          "videogameId": 1,
          "name": "Project Shark",
          "type": "Shooter",
          "releaseDate": 1986,
          "rating": 2.2,
          "consoleExclusive": false
      },
      {
          "videogameId": 2,
          "name": "Big Fighter",
          "type": "Battle Royal",
          "releaseDate": 2009,
          "rating": 3.2,
          "consoleExclusive": false
      },
      {
          "videogameId": 3,
          "name": "Project Fighter",
          "type": "Casual",
          "releaseDate": 1997,
          "rating": 3.1,
          "consoleExclusive": false
      },
      {
          "videogameId": 4,
          "name": "Super Road",
          "type": "RPG",
          "releaseDate": 1996,
          "rating": 3.7,
          "consoleExclusive": false
      },
      {
          "videogameId": 5,
          "name": "Project Coal",
          "type": "Puzzle",
          "releaseDate": 1988,
          "rating": 3.8,
          "consoleExclusive": true
      },
      {
          "videogameId": 6,
          "name": "Yeeter Fighter",
          "type": "Idle",
          "releaseDate": 2013,
          "rating": 2.1,
          "consoleExclusive": false
      },
      {
          "videogameId": 7,
          "name": "World of Tree",
          "type": "Party",
          "releaseDate": 2016,
          "rating": 2.1,
          "consoleExclusive": false
      },
      {
          "videogameId": 8,
          "name": "Super Kitten",
          "type": "Action",
          "releaseDate": 1987,
          "rating": 3.1,
          "consoleExclusive": true
      },
      {
          "videogameId": 9,
          "name": "Fight Kitten",
          "type": "Battle Royal",
          "releaseDate": 2018,
          "rating": 2.8,
          "consoleExclusive": false
      },
      {
          "videogameId": 10,
          "name": "Fight Bob",
          "type": "Action",
          "releaseDate": 2009,
          "rating": 2.0,
          "consoleExclusive": false
      }]
  }

}
