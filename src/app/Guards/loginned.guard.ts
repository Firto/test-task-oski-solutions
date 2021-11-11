import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, CanActivateChild, CanLoad, Route } from '@angular/router';
import { UserService } from '../Services/user.service';
//import { ToastrService } from 'ngx-toastr';
import { GuardService } from '../Services/guard.service';
import { Toaster } from 'ngx-toast-notifications';

@Injectable({ providedIn: 'root' })
export class LoginnedGuard implements CanActivate, CanActivateChild, CanLoad {
    constructor(private userService: UserService,
                private toastrService: Toaster,
                private router: Router,
                private _guardService: GuardService) { }

    canActivateChild(childRoute: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        return this.canActivate(childRoute, state);
    }

    canLoad(route: Route): boolean {
      return this.canActivate(null, null);
    }

    canActivate(route: ActivatedRouteSnapshot | null, state: RouterStateSnapshot | null) {
        if (!this._guardService.getState())
            return true;
        if (this.userService.user)
            return true;
        this.toastrService.open("No permission to page!", {type: 'danger'});
        this.router.navigateByUrl('/auth?returnUrl=' + encodeURI(window.location.pathname));
        return false;
    }
}
