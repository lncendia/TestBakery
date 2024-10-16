import {PredictedCost} from './predicted-cost';

/**
 * Интерфейс, представляющий данные булочки.
 */
export interface Bun {

  /**
   * Идентификатор булочки.
   */
  id: string;

  /**
   * Тип булочки.
   */
  type:  "Baguette" | "Croissant" | "Loaf" | "Pretzel" | "Smetannik" ;

  /**
   * Время выпечки булочки.
   */
  bakeTime: string;

  /**
   * Начальная стоимость булочки.
   */
  initialCost: number;

  /**
   * Текущая стоимость булочки.
   */
  currentCost: number;

  /**
   * Прогнозируемая стоимость булочки.
   */
  predictedCost?: PredictedCost;
}
