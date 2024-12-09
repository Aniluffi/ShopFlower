document.getElementById('adult-min-price').addEventListener('input', updatePriceValues);
document.getElementById('adult-max-price').addEventListener('input', updatePriceValues);

function updatePriceValues() {
    const adultMin = document.getElementById('adult-min-price').value;
    const adultMax = document.getElementById('adult-max-price').value;

    document.getElementById('adult-price-values').innerText = `${adultMin} - ${adultMax}`;
}


document.getElementById('apply-filter').addEventListener('click', () => {
    const minPrice = document.getElementById('adult-min-price').value;
    const maxPrice = document.getElementById('adult-max-price').value;

    const filterData = {
        PriceMin: minPrice,
        PriceMax: maxPrice,
    };

    console.log('Отправленные данные ', filterData);

    fetch('/Shop/Filter', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(filterData),
    })
        .then((response) => {
            if (!response.ok) {
                throw new Error("Ошибка фильтрации");
            }
            return response.json();
        })
        .then((data) => {
            console.log("Результат фильтрации:", data);

            // Обновляем товары на странице
            dataDisplay(data);
        })
        .catch((error) => {
            console.log("Ошибка:", error);
        });
});

function dataDisplay(data) {
    const productList = document.querySelector('.shop_wrapper');
    productList.innerHTML = ''; // Очищаем контейнер товаров

    // Проверяем, что данные и товары существуют
    if (!data || !data.products || data.products.length === 0) {
        const noProductMessage = ` <style>
                                .no-products {
                                    display: flex;
                                    justify-content: center;
                                    align-items: center;
                                    height: 200px; /* Высота блока, можно настроить */
                                    text-align: center;
                                }

                                    .no-products p {
                                        font-size: 18px; /* Размер шрифта */
                                        color: #555; /* Цвет текста */
                                        font-weight: bold; /* Жирность текста */
                                        margin: 0;
                                    }
                            </style>
                            <div class="no-products">
                                <p>По данному фильтру ничего не найдено</p>
                            </div>`;
        productList.innerHTML = noProductMessage; // Если нет товаров, выводим сообщение
    } else {
        // Перебираем товары и добавляем их на страницу
        data.products.forEach((product) => {
            const productHTML = `
                <div class="col-lg-3 col-md-4 col-12" onclick="window.location.href = '/Shop/ProductDetails/${product.id}'">
                    <article class="single_product">
                        <figure>
                            <div class="product_thumb">
                                <img src="/img/product/${product.img}" alt="фото товара">
                            </div>
                            <div class="product_content">
                                <h4 class="product_name">
                                    <a href="#">${product.name}</a>
                                </h4>
                                <div class="price_box">
                                    <span class="current_price">£${product.price}</span>
                                </div>
                            </div>
                        </figure>
                    </article>
                </div>`;
            productList.innerHTML += productHTML; // Добавляем каждый товар
        });
    }
}