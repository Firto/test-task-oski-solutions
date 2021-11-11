import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { filter } from 'rxjs/operators';
import { LoginService } from './Services/login.service';
import { UserService } from './Services/user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'test-task-oski-solutions';
  previousUrl: string;
  currentUrl: string;
  constructor(public usrService: UserService,
              public loginService: LoginService,
              public ntfService: Toaster,
              private router: Router)
  {

  }

  ngOnInit() : void {
    this.router.events.pipe(
      filter((event) => event instanceof NavigationEnd)
    ).subscribe((event: NavigationEnd) => {
      this.previousUrl = this.currentUrl;
      this.currentUrl = event.url;
    });
  }

  public logout(): void
  {
    this.loginService.logout().subscribe(() => {
      this.router.navigateByUrl("/auth");
      this.ntfService.open("Successfully logout!", {type: "success"});
    });
  }
}
