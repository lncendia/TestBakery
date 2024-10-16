import {Bun} from './bun';

/**
 * Интерфейс, представляющий коллекцию данных булочек.
 */
export interface Buns {

  /**
   * Коллекция данных булочек.
   */
  buns: Bun[];

  /**
   * Общее количество булочек.
   */
  totalCount: number;
}
