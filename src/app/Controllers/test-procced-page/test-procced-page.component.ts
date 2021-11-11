import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { TestModel } from 'src/app/Models/Tests/test.model';
import { TestsService } from 'src/app/Services/tests.service';

@Component({
  selector: 'app-test-procced-page',
  templateUrl: './test-procced-page.component.html',
  styleUrls: ['./test-procced-page.component.css']
})
export class TestProccedPageComponent implements OnInit {
  id: number;
  model: TestModel;
  agreed: boolean = false;
  constructor(
    private testsService: TestsService,
    private router: Router,
    private activateRoute: ActivatedRoute) {

    if (Number.isInteger(+activateRoute.snapshot.params['id']))
      this.id = +activateRoute.snapshot.params['id'];
    else
      this.router.navigate(['/', 'list-of-tests']);
  }

  public startSession(): void {
    this.testsService.start(this.id).subscribe(ses => {
      this.router.navigate(['/', 'test', ses.id, '0']);
    });
  }

  ngOnInit(): void {
    this.testsService.getById(this.id)
      .subscribe(
        m => {
          this.model = m;
        },
        () => {
          this.router.navigate(['/', 'list-of-tests']);
        }
      );
  }

}
