import { NgModule } from '@angular/core';
import { Routes, RouterModule, UrlSegment, UrlSegmentGroup, UrlMatchResult } from '@angular/router';
import { AuthComponent } from './Controllers/auth/auth.component';
import { ListOfTestsComponent } from './Controllers/list-of-tests/list-of-tests.component';
import { TestProccedPageComponent } from './Controllers/test-procced-page/test-procced-page.component';
import { TestProcessedComponent } from './Controllers/test-processed/test-processed.component';
import { TestQuestionComponent } from './Controllers/test-question/test-question.component';
import { TestResultComponent } from './Controllers/test-result/test-result.component';
import { LoginRegisterGuard } from './Guards/login-register.guard';
import { LoginnedGuard } from './Guards/loginned.guard';


export const routes: Routes = [
  {
    path: 'auth',
    component: AuthComponent,
    canActivate: [LoginRegisterGuard],
    runGuardsAndResolvers: 'always'
  },
  {
    path: 'list-of-tests',
    component: ListOfTestsComponent,
    canActivate: [LoginnedGuard],
    runGuardsAndResolvers: 'always',
  },
  {
    path: 'proceed/:id',
    component: TestProccedPageComponent,
    canActivate: [LoginnedGuard],
    runGuardsAndResolvers: 'always'
  },
  {
    path: 'test/:id',
    component: TestProcessedComponent,
    canActivate: [LoginnedGuard],
    runGuardsAndResolvers: 'always'
  },
  {
    path: 'test/:id',
    component: TestProcessedComponent,
    canActivate: [LoginnedGuard],
    runGuardsAndResolvers: 'always',
    children: [
      {
        path: 'result',
        component: TestResultComponent,
      },
      {
        path: ':questionId',
        component: TestQuestionComponent,
      }
    ]
  },
  { path: '',   redirectTo: '/auth', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
