import { Component, Input, OnInit } from '@angular/core';
import { IVideogame } from '../shared/videogame.model';

@Component({
  selector: 'videogame-thumbnail',
  templateUrl: './videogame-thumbnail.component.html',
  styleUrls: ['./videogame-thumbnail.component.css']
})
export class VideogameThumbnailComponent implements OnInit {

  @Input() videogame!:IVideogame;

  constructor() { }

  ngOnInit(): void {
  }

}
