import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Toaster } from 'ngx-toast-notifications';
import { first } from 'rxjs/operators';
import { LoginService } from 'src/app/Services/login.service';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css']
})
export class AuthComponent implements OnInit {
  returnUrl: string = "";
  loginForm = this.formBuilder.group({
    login: ['', Validators.required],
    password: ['', Validators.required],
  });

  constructor(
      private formBuilder: FormBuilder,
      private route: ActivatedRoute,
      private router: Router,
      private authService: LoginService,
      private toastrService: Toaster,
  ) {

  }

  ngOnInit() {
      this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/list-of-tests';
  }

  get f() { return this.loginForm.controls; }

  onSubmit() {
      // stop here if form is invalid
      if (this.loginForm.invalid)
        return;
      this.authService.login(this.f["login"].value, this.f["password"].value)
          .subscribe(
            val => {
              this.router.navigateByUrl(this.returnUrl);
              this.toastrService.open("Succesful loginned!", {type: 'success'});
            }
          );
  }

}
