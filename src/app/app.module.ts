import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppComponent } from './app.component';
import { TestProccedPageComponent } from './Controllers/test-procced-page/test-procced-page.component';
import { TestQuestionComponent } from './Controllers/test-question/test-question.component';
import { TestAnswerComponent } from './Controllers/test-answer/test-answer.component';
import { AuthComponent } from './Controllers/auth/auth.component';
import { ListOfTestsComponent } from './Controllers/list-of-tests/list-of-tests.component';
import { AgreementPageComponent } from './Controllers/agreement-page/agreement-page.component';
import { MyLocalStorageService } from './Services/my-local-storage.service';
import { BaseService } from './Services/base.service';
import { LoginService } from './Services/login.service';
import { LoaderService } from './Services/loader.service';
import { DeviceUUIDService } from './Services/device-uuid.service';
import { CryptService } from './Services/crypt.service';
import { GuardService } from './Services/guard.service';
import { AppRoutingModule } from './app-routing.module';
import { CommonModule } from '@angular/common';
import { ToastNotificationsModule } from 'ngx-toast-notifications';
import { BasicAuthInterceptor } from './Intercepors/basic-auth.interceptor';
import { ErrorInterceptor } from './Intercepors/user-error.interceptor';
import { LoaderInterceptor } from './Intercepors/loader.interceptor';
import { LoaderComponent } from './Controllers/loader/loader.component';
import { TestsService } from './Services/tests.service';
import { TestProcessedComponent } from './Controllers/test-processed/test-processed.component';
import { TestResultComponent } from './Controllers/test-result/test-result.component';
import { TestQuestionResultComponent } from './Controllers/test-question-result/test-question-result.component';

@NgModule({
  declarations: [
    AppComponent,
    LoaderComponent,
    AuthComponent,
    ListOfTestsComponent,
    AgreementPageComponent,
    TestProccedPageComponent,
    TestQuestionComponent,
    TestAnswerComponent,
    TestProcessedComponent,
    TestResultComponent,
    TestQuestionResultComponent
  ],
  imports: [
    BrowserAnimationsModule,
    CommonModule,
    ReactiveFormsModule,
    HttpClientModule,
    AppRoutingModule,
    ToastNotificationsModule
  ],
  providers: [
    MyLocalStorageService,
    BaseService,
    LoginService,
    LoaderService,
    DeviceUUIDService,
    CryptService,
    GuardService,
    TestsService,
    { provide: HTTP_INTERCEPTORS, useClass: BasicAuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: LoaderInterceptor, multi: true }

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
