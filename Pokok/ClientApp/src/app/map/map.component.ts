import { Component, OnInit } from '@angular/core';
import { latLng, tileLayer } from 'leaflet';

declare var L;
declare var HeatmapOverlay;

@Component({
  selector: 'app-map',
  templateUrl: './map.component.html',
  styleUrls: ['./map.component.css']
})

export class MapComponent implements OnInit {

  data = {
    data: []
  };

  heatmapLayer = new HeatmapOverlay({
    radius: 2,
    maxOpacity: 0.8,
    scaleRadius: true,
    useLocalExtrema: true,
    latField: 'lat',
    lngField: 'lng',
    valueField: 'count'
  });

  options = {
    layers: [
      L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; OpenStreetMap contributors'
      }),
      this.heatmapLayer
    ],
    zoom: 15,
    center: latLng([3.0726965, 101.5899273])
  };

  onMapReady(map: L.Map) {
    map.on('mousemove', (event: L.LeafletMouseEvent) => {
      this.data.data.push({
        lat: event.latlng.lat,
        lng: event.latlng.lng,
        count: 1
      });

      this.heatmapLayer.setData(this.data);
    });
  }

  ngOnInit(): void {
  }

}
