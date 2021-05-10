import { Component, OnInit, Inject } from '@angular/core';
import * as L from 'leaflet';
import { HttpClient } from '@angular/common/http';
import 'leaflet.heat/dist/leaflet-heat.js'
import { FormGroup, FormBuilder, ValidatorFn, AbstractControl, Validators } from '@angular/forms';
import { CustomValidatorMatcher, CustomValidators } from '../custom-validators'

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css'],
})

export class MapComponent implements OnInit {

  private dataPoints: HeatmapDataPoints[];
  private baseUrl: string;
  customMatcher = new CustomValidatorMatcher

  treeForm: FormGroup;
  submitted = false;

  constructor(@Inject('BASE_URL') baseUrl: string, private http: HttpClient, private fb: FormBuilder) {
    this.baseUrl = baseUrl;
  }

  options = {
    layers: [
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
      })
    ],
    gradient: { 0.4: 'grey', 0.65: 'brown', 1: 'lime' },
    zoom: 15,
    center: L.latLng([3.0726965, 101.5899273])
  };

  onMapReady(map: L.Map) {

    this.http.get<HeatmapDataPoints[]>(this.baseUrl + 'api/tree').subscribe(result => {
      this.dataPoints = result;

      let addressPoints: [number, number, number][] = this.dataPoints.map(function (item) {
        return [item.latitude, item.longitude, item.weight];
      });

    (L as any).heatLayer(addressPoints, { radius: 25 }).addTo(map);

    }, error => console.error(error));
  }

  ngOnInit(): void {
    this.treeForm = this.fb.group({
      latitude: ['', CustomValidators.latitudeValidation],
      longitude: ['', CustomValidators.longitudeValidation],
      species: 'Dunno'
    })
  }

  get latitude() {
    return this.treeForm.get('latitude');
  }

  get longitude() {
    return this.treeForm.get('longitude');
  }
}

interface HeatmapDataPoints {
  latitude: number;
  longitude: number;
  weight: number;
}
