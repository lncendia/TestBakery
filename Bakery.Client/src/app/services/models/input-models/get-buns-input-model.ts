/**
 * Интерфейс, представляющий входную модель для получения булочек.
 */
export interface GetBunsInputModel {
  /**
   * Лимит.
   */
  limit: number;

  /**
   * Смещение.
   */
  offset?: number;
}
