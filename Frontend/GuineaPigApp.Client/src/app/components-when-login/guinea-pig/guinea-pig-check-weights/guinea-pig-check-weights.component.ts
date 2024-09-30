import { Component, OnInit, ElementRef, Renderer2 } from '@angular/core';
import { Chart, registerables } from 'chart.js';
import { ToastrService } from 'ngx-toastr';
import { finalize } from 'rxjs';
import { GuineaPigDto } from 'src/app/_models/guinea-pig-dto';
import { GuineaPigWeightsDto } from 'src/app/_models/guinea-pigs-weights-dto';
import { GuineaPigService } from 'src/app/_service/guinea-pig.service';
import { ThemeHelper } from 'src/app/_service/themeHelper.service';
import { TokenService } from 'src/app/_service/token.service';
import { BaseComponent } from 'src/app/_shared/base.component';

Chart.register(...registerables);

@Component({
  selector: 'app-guinea-pig-check-weights',
  templateUrl: './guinea-pig-check-weights.component.html',
  styleUrls: ['./guinea-pig-check-weights.component.css'],
})
export class GuineaPigCheckWeightsComponent
  extends BaseComponent
  implements OnInit
{
  override cloudText: string = 'Ej! Moja waga przecież jest dobra!';

  public config: any;
  chart: any;
  model: GuineaPigDto = new GuineaPigDto();
  pigs: string[] = [];
  email: string = '';
  guineaPigs: GuineaPigDto[] = [];
  selectedPig: GuineaPigDto = new GuineaPigDto();
  weights: GuineaPigWeightsDto[] = [];

  constructor(
    guineaPigService: GuineaPigService,
    public themeHelper: ThemeHelper,
    private tokenService: TokenService,
    private toastr: ToastrService,
    private renderer: Renderer2,
    private el: ElementRef
  ) {
    super(guineaPigService);
  }

  override ngOnInit(): void {
    super.ngOnInit();
    this.getEmailFromToken();
    this.getGuineaPigs();
  }

  getEmailFromToken() {
    this.email = this.tokenService.getEmailFromToken();
  }

  getGuineaPigs() {
    this.guineaPigService.getGuineaPigs(this.email).subscribe({
      next: (response) => {
        this.guineaPigs = response;
      },
      error: () => {},
    });
  }

  getGuineaPigWeights() {
    if (!this.selectedPig || !this.selectedPig.name) {
      this.toastr.error('Nie wybrano profilu świnki');
      return;
    }

    this.email = this.tokenService.getEmailFromToken();

    this.guineaPigService
      .getGuineaPigWeights(this.email, this.selectedPig.name)
      .pipe(
        finalize(() => {
          this.updateChartData();
        })
      )
      .subscribe({
        next: (response) => {
          this.weights = response;
        },
        error: () => this.toastr.error('Nie wybrano profilu świnki!'),
      });
  }

  updateChartData() {
    if (!this.weights.length || !this.selectedPig.name) {
      return;
    }

    const labels = this.weights.map((weight) => weight.date);
    const data = this.weights.map((weight) => weight.weight);

    if (this.chart) {
      this.chart.destroy();
    }

    const canvasContainer =
      this.el.nativeElement.querySelector('#canvas-container');

    if (canvasContainer.firstChild) {
      canvasContainer.removeChild(canvasContainer.firstChild);
    }

    const canvas = this.renderer.createElement('canvas');
    this.renderer.setStyle(canvas, 'display', 'flex');
    this.renderer.setStyle(canvas, 'justify-content', 'center');
    this.renderer.setStyle(canvas, 'align-items', 'center');
    this.renderer.setStyle(canvas, 'margin-top', '20px');
    this.renderer.setStyle(canvas, 'margin-left', '50px');
    this.renderer.setStyle(canvas, 'margin-right', '50px');
    this.renderer.setStyle(canvas, 'background-color', 'white');
    this.renderer.setStyle(canvas, 'height', '200px');
    this.renderer.setStyle(canvas, 'width', '80%');
    this.renderer.appendChild(canvasContainer, canvas);
    this.renderer.appendChild(canvasContainer, canvas);

    this.config = {
      type: 'line',
      data: {
        labels: labels,
        datasets: [
          {
            label: this.selectedPig.name,
            data: data,
            backgroundColor: 'blue',
          },
        ],
      },
      options: {
        scales: {
          y: {
            beginAtZero: true,
          },
        },
        maintainAspectRatio: false,
      },
    };

    this.chart = new Chart(canvas, this.config);
  }
}
