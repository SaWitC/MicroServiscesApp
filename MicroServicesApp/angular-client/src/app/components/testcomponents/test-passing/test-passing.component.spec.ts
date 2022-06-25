import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TestPassingComponent } from './test-passing.component';

describe('TestPassingComponent', () => {
  let component: TestPassingComponent;
  let fixture: ComponentFixture<TestPassingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TestPassingComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TestPassingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
