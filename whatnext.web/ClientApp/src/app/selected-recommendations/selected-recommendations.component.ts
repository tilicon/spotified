import { Component, Inject, Input, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Track } from "../models/track";
import { MockTracks } from '../mock/mock-tracks';

@Component({
  selector: 'app-selected-recommendations',
  templateUrl: './selected-recommendations.component.html',
  styleUrls: ['./selected-recommendations.component.css']
})
export class SelectedRecommendationsComponent implements OnInit{
  @Input() selectedRecommendations: Track[] = [];
  tracks: Track[] = [];

  constructor() {
    this.tracks = MockTracks;
  }

  ngOnInit(): void {
  }

  //constructor(http: HttpClient, @Inject('BASE_URL') baseUrl: string) {
  //  http.get<Category[]>(baseUrl + 'api/recommendations/categories').subscribe(result => {
  //    this.categories = result;
  //  }, error => console.error(error));
  //}
}
