import { Component, Input, OnInit } from '@angular/core';
import { CommonUtilsService } from '../common/common-utils.service';
import { IVideogame } from './shared/videogame.model';
import { VideogameService } from './shared/videogame.service';

@Component({
  selector: 'videogame-thumbnail',
  templateUrl: './videogame-thumbnail.component.html',
  styleUrls: ['./videogame-thumbnail.component.css']
})
export class VideogameThumbnailComponent implements OnInit {

  editMode? : boolean;
  tempVideogame! : IVideogame;

  @Input() videogame!:IVideogame;

  constructor(private videogameService: VideogameService, private commonUtilsService : CommonUtilsService) { }

  ngOnInit(): void {
    this.editMode = false;    
    this.tempVideogame = Object.assign({}, this.videogame);
  }

  enableEditMode(): void {
    this.editMode = !this.editMode;
  }

  confirmEdit(): void {
    
    this.videogame = this.tempVideogame;
    this.videogameService.updateVideogame(this.videogame).subscribe(() => {
      this.enableEditMode();
      this.ngOnInit();
    });
    
  }

  cancelEdit(): void {

    this.enableEditMode();
    this.ngOnInit();
  }

}