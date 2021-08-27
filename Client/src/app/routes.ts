import { Routes } from '@angular/router'

import { WelcomeComponent } from './welcome/welcome.component';
import { VideogameListComponent} from './videogames/videogame-list.component';

import { Error404Component } from './errors/404.component'

export const appRoutes:Routes = [
  { path: 'welcome', component: WelcomeComponent},
  { path: 'videogames', component: VideogameListComponent},
  { path: 'videogames/:id', component: VideogameListComponent },
  { path: '404', component: Error404Component },
  { path: '', redirectTo: '/welcome', pathMatch: 'full'},
]