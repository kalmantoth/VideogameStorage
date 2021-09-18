import { Component, Input, OnInit } from '@angular/core';
import { IVideogame } from './shared/videogame.model';

@Component({
  selector: 'videogame-thumbnail',
  templateUrl: './videogame-thumbnail.component.html',
  styleUrls: ['./videogame-thumbnail.component.css']
})
export class VideogameThumbnailComponent implements OnInit {

  editMode? : boolean;
  tempVideogame! : IVideogame;
  origVideogame! : IVideogame;

  @Input() videogame!:IVideogame;

  constructor() { }

  ngOnInit(): void {
    this.editMode = false;    
    this.origVideogame = Object.create(this.videogame);
    this.tempVideogame = Object.create(this.videogame);
  }

  enableEditMode(): void {
    this.editMode = !this.editMode;
  }

  confirmEdit(): void {
    
    this.videogame = this.tempVideogame;
    this.enableEditMode();
    this.ngOnInit();
  }

  cancelEdit(): void {

    this.videogame = this.origVideogame;
    this.enableEditMode();
    this.ngOnInit();
  }

}