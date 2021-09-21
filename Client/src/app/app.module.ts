import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { PreloadAllModules, RouterModule } from '@angular/router'
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { WelcomeComponent } from './welcome/welcome.component';
import { VideogameListComponent} from './videogames/videogame-list.component';
import { VideogameThumbnailComponent } from './videogames/videogame-thumbnail.component'
import { NavBarComponent } from './nav/nav-bar.component';
import { Error404Component } from './errors/404.component'

import { appRoutes } from './routes';



@NgModule({
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule.forRoot(appRoutes, {preloadingStrategy: PreloadAllModules}),
    HttpClientModule
  ],
  declarations: [
    AppComponent,
    NavBarComponent,
    WelcomeComponent,
    VideogameListComponent,
    Error404Component,
    VideogameThumbnailComponent
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
