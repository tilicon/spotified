import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RecommendationsComponent } from './recommendations.component';

describe('RecommendationsComponent', () => {
  let component: RecommendationsComponent;
  let fixture: ComponentFixture<RecommendationsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RecommendationsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RecommendationsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should display a title', async(() => {
    const titleText = fixture.nativeElement.querySelector('h1').textContent;
    expect(titleText).toEqual('Music Recommendations');
  }));

  it('should display categories on the recommendations landing page', async(() => {
    //const countElement = fixture.nativeElement.querySelector('strong');
    //expect(countElement.textContent).toEqual('0');

    //const incrementButton = fixture.nativeElement.querySelector('button');
    //incrementButton.click();
    //fixture.detectChanges();
    //expect(countElement.textContent).toEqual('1');
  }));
});
