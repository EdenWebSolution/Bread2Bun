import { animate, state, style, transition, trigger } from '@angular/animations';


export let slideFromBottom = trigger('slideFromBottom', [
  state('void', style({ 'padding-top': '20px', opacity: '0' })),
  state('*', style({ 'padding-top': '0px', opacity: '1' })),
  transition(':enter', [
    animate('0.33s ease-out', style({ opacity: '1', 'padding-top': '0px' }))
  ])
]);


export let slideFromUp = trigger('slideFromUp', [
  state('void', style({ 'margin-top': '-10px', opacity: '0' })),
  state('*', style({ 'margin-top': '0px', opacity: '1' })),
  transition(':enter', [
    animate('0.2s ease-out', style({ opacity: '1', 'margin-top': '0px' }))
  ])
]);
