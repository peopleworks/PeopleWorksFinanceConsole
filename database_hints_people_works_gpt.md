# 🧠 PeopleWorks GPT – Guía de Datos para Estadísticas Financieras RD

Esta guía ofrece contexto y directrices para que **PeopleWorks GPT** pueda interpretar y consultar correctamente la base de datos del proyecto **PeopleWorks Finance Console** con datos extraídos del API oficial de la Superintendencia de Bancos de la República Dominicana.

---

## 📆 Rango de Fechas Disponible

> **Muy Importante:** Solo hay datos disponibles desde **enero 2024** hasta **mayo 2025**.
>
> El campo `Periodo` representa una **combinación de año y mes** en el formato `YYYY-MM`. Por ejemplo:
>
> * `2024-01`: Enero 2024
> * `2025-05`: Mayo 2025
>
> Este campo es **clave para cualquier comparación temporal** en las tablas.

---

## 📊 Tablas Principales

### 1. `Entidad`

**Contiene las instituciones financieras reguladas.**

| Campo         | Descripción                                 |
| ------------- | ------------------------------------------- |
| `Id`          | Identificador interno autonumérico          |
| `Nombre`      | Nombre oficial de la institución financiera |
| `TipoEntidad` | Clasificación: AAyP, BM, EP, etc.           |

> **AAyP**: Asociaciones de Ahorros y Préstamos
> **BM**: Bancos Múltiples
> **EP**: Entidades Públicas

---

### 2. `Captacion`

**Registra los montos captados por cada entidad, persona y tipo de producto financiero.**

| Campo         | Descripción                                      |
| ------------- | ------------------------------------------------ |
| `Periodo`     | Formato `YYYY-MM`                                |
| `TipoEntidad` | Tipo de institución                              |
| `Entidad`     | Nombre de la entidad                             |
| `Persona`     | Física o Jurídica                                |
| `Producto`    | Producto financiero (Ahorro, Plazo, Vista, etc.) |
| `Monto`       | Monto captado                                    |
| `Divisa`      | Moneda (Ej. PESO DOMINICANO, USD)                |

---

### 3. `Cartera`

**Detalle de los créditos otorgados por entidad, tipo, sector y ubicación.**

| Campo             | Descripción                       |
| ----------------- | --------------------------------- |
| `Periodo`         | Periodo en `YYYY-MM`              |
| `TipoEntidad`     | Tipo de institución               |
| `Entidad`         | Nombre de la institución          |
| `TipoCredito`     | Tipo de crédito                   |
| `SectorEconomico` | Sector económico del beneficiario |
| `Region`          | Región geográfica                 |
| `Provincia`       | Provincia                         |
| `Moneda`          | Tipo de moneda                    |
| `Deuda`           | Monto total de la deuda           |
| `DeudaCapital`    | Porción de capital de la deuda    |
| `TasaPorDeuda`    | Tasa promedio calculada           |

---

### 4. `EstadoFinanciero`

**Estado general de las cuentas por entidad.**

| Campo         | Descripción                |
| ------------- | -------------------------- |
| `Periodo`     | Fecha (YYYY-MM)            |
| `TipoEntidad` | Tipo de institución        |
| `Entidad`     | Nombre de la entidad       |
| `Cuenta`      | Nombre del rubro contable  |
| `Valor`       | Monto asociado a la cuenta |

---

### 5. `Indicador`

**Métricas clave como rentabilidad, liquidez, solvencia, etc.**

| Campo         | Descripción                               |
| ------------- | ----------------------------------------- |
| `Periodo`     | Mes de publicación (YYYY-MM)              |
| `TipoEntidad` | Clasificación del tipo de entidad         |
| `Entidad`     | Nombre de la institución                  |
| `Indicador`   | Nombre del indicador (ej. ROE, Morosidad) |
| `Valor`       | Valor numérico del indicador              |

---

## 🤔 Preguntas de ejemplo para IA

* ¿Cuál fue la entidad con mayor captación en enero de 2024?
* Muestra la evolución mensual de los indicadores de rentabilidad (ROA y ROE) de las asociaciones AAyP.
* Compara la deuda total por sector económico entre 2024 y 2025.
* ¿Cuáles entidades públicas tienen los indicadores de liquidez más bajos?
* ¿Cuál es el crecimiento mensual de la cartera de crédito en la región Norte?
* Lista los indicadores financieros disponibles para Bancos Múltiples.

---

## 🔧 Reglas de consulta para AI

1. Siempre filtrar por `Periodo` para datos mensuales.
2. Usar `LIKE '%texto%'` para buscar en `Entidad`, `Cuenta` o `Indicador`.
3. Para comparar años, agrupar por `LEFT(Periodo, 4)`.
4. En indicadores, evitar NULL y agrupar por mes para tendencias.
5. Convertir `Periodo` a `DATE` si se desea hacer comparaciones precisas.

---

## 🖊️ Sugerencias de Prompt

* Compara los indicadores de liquidez entre las entidades EP y BM.
* Muestra los productos financieros que más captan personas jurídicas.
* Lista las provincias con mayor cartera de créditos para microempresas.
* Genera un resumen mensual del estado financiero para AAyP.

---

## 📚 Diccionario de Códigos y Abreviaciones

| Código | Significado                         |
| ------ | ----------------------------------- |
| AAyP   | Asociaciones de Ahorros y Préstamos |
| BAyC   | Bancos de Ahorro y Crédito          |
| BM     | Bancos Múltiples                    |
| CC     | Corporaciones de Crédito            |
| EP     | Entidades Públicas                  |
| TODOS  | Todo el sistema financiero          |

---

## 🏛️ Nota técnica

* Todas las fechas deben analizarse como `Periodo` en formato `YYYY-MM`.
* Las monedas pueden incluir "PESO DOMINICANO" o "USD".
* Algunas entidades pueden tener datos nulos en algunos meses.
* El sistema está optimizado para integrarse con herramientas como Power BI o GPT-4 para análisis natural.

---

**PeopleWorks GPT** fue entrenado para entender esta estructura y responder preguntas en lenguaje natural relacionadas con indicadores y comportamiento financiero de instituciones en RD.

✉️ Contacto: [support@peopleworksgpt.com](mailto:support@peopleworksgpt.com)
