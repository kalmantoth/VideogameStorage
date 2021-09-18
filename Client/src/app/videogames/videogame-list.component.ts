import { Component, OnInit } from '@angular/core';
import { IVideogame } from './shared/videogame.model';
import { VideogameService } from './shared/videogame.service';

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

  videogames: IVideogame[] = [];

  constructor(private videogameService: VideogameService) { }

  ngOnInit(): void {
      this.getVideogames();
  }

  getVideogames(): void{
    this.videogameService.getVideogames()
            .subscribe(videogames => this.videogames = videogames);
  }

}
