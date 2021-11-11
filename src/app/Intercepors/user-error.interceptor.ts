import { Injectable, Injector, Inject, forwardRef } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';


import { APIResult } from 'src/app/Models/api.result.model';
import { tap, map, take } from 'rxjs/operators';
import { UserService } from '../Services/user.service';
import { Toaster } from 'ngx-toast-notifications';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

    constructor(private _userService: UserService,
                private _toastrService: Toaster) {
    }

    intercept(request: HttpRequest<APIResult>, next: HttpHandler): Observable<HttpEvent<APIResult>> {
        return next.handle(request).pipe(
            map((event: HttpEvent<APIResult>) => {
                if (event instanceof HttpResponse)
                {
                    if (event.body!.ok)
                        return <any>event.clone({body: event.body!.result});

                    console.log(event.body);
                    switch (event.body!.result.id){
                        case "inc-refresh-token":
                        case "already-login":
                        case "no-login":
                        case "base-account-service":
                        case "inc-uuid":
                        case "no-uuid":
                            if (this._userService.user)
                                this._userService.localLogout();
                            break;
                        case "v-dto-invalid":
                        case "inc-token":
                            throw event.body!.result;
                    }
                    this._toastrService.open(event.body!.result.message, {type: 'danger'});
                    throw event.body!.result;
                }
                return event;
            }
        ));
    }
}
