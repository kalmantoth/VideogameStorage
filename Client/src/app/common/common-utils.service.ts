import { Injectable } from '@angular/core';
import { Router } from "@angular/router";


@Injectable({
    providedIn: 'root'
  })
export class CommonUtilsService {

    constructor(private router: Router) { }

    public reloadCurrentRoute() : void {
        const currentUrl = this.router.url;
        this.router.navigateByUrl('/', {skipLocationChange: true}).then(() => {
            this.router.navigate([currentUrl]);
        });
    }
}