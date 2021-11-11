import { BehaviorSubject, Observable, Subject } from 'rxjs';
import { User } from '../Models/user.model';
import { Injectable, Injector } from '@angular/core';
import { ActivatedRoute, Router, RouterStateSnapshot } from '@angular/router';
import { Loginned } from '../Models/loginned.model';
import { CryptService } from './crypt.service';
import { DeviceUUIDService } from './device-uuid.service';
import { MyLocalStorageService } from './my-local-storage.service';
import { Toaster } from 'ngx-toast-notifications';
import { Route } from '@angular/compiler/src/core';

@Injectable({providedIn: 'root'})
export class UserService {
    private _user: BehaviorSubject<User | null> = new BehaviorSubject<User | null>(null);
    public get userObs(): Observable<User | null> {return this._user;};

    public get user(): User | null {return this._user.value};
    public set user(usr: User | null) {
        this._localStorage.write("user", usr);
        this._user.next(usr);
    };
    constructor(private _localStorage: MyLocalStorageService,
                private _uuidService: DeviceUUIDService,
                private _toast: Toaster,
                private _router: Router){
        if (_localStorage.isIssetKey('user')){
            this._user.next(_localStorage.read('user'));
        }
    }

    public writeInStorage(usr: User){
        this._localStorage.write("user", usr);
    }

    forceRunAuthGuard(): void {
        /*console.log(this._route);
        if (this._route.children.length && this._route.children['0'].snapshot._routeConfig.canActivate) {
            const curr__route = this._route.children[ '0' ];
            const AuthGuard = curr__route.snapshot._routeConfig.canActivate[ '0' ];
            const authGuard = this._injector.get(AuthGuard);
            const _routerStateSnapshot: RouterStateSnapshot = Object.assign({}, curr__route.snapshot, {url: "/"+curr__route.snapshot.url[0]});
            authGuard.canActivate(curr__route.snapshot, _routerStateSnapshot);
        }*/
    }

    // local

    localLogout(): void {
        this.user = null;
        //this.forceRunAuthGuard();
    }

    localLogin(login: Loginned): void {
        const user = new User();
        user.login = login;
        this.user = user;
    }

    localSetLoginTokens(login: Loginned): void {
        const user = this.user;
        user.login = login;
        this.user = user;
    }
}
